using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using TVShowTracker.API.Data;
using TVShowTracker.API.Mapping;
using TVShowTracker.API.Repositories;
using TVShowTracker.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the JWT secret key from configuration
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new Exception("Jwt:Key not set");

//Register authentication with diagnostic events
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        // For development only: allow HTTP so Swagger/Postman work locally
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        //Configure how JWT tokens are validated
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,             //Skip issuer validation (simplified setup)
            ValidateAudience = false,           //Skip audience validation
            ValidateIssuerSigningKey = true,    //Validate the signing key integrity
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)), //Secret Key
            ClockSkew = TimeSpan.Zero,          //No tolerance for expiry during testing
            ValidateLifetime = true             //Ensure tokens expire properly
        };
    });

//Enable [Authorize] attribute to secure endpoints
builder.Services.AddAuthorization();

//Add services to DI
builder.Services.AddScoped<AuthService>();

//Repositories
builder.Services.AddScoped<ITVShowRepository, TVShowRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Services
builder.Services.AddScoped<ITVShowService, TVShowService>();
builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IExportService, ExportService>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Controllers
builder.Services.AddControllers();

//Swagger + JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Enter JWT token. Click 'Authorize' and paste the token (no 'Bearer ' prefix).",
        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        }
    };

    //Register the Bearer security definition and requirement
    c.AddSecurityDefinition("Bearer", jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    { jwtSecurityScheme, Array.Empty<string>() }
});
    //Include example filters (for better Swagger examples)
    c.ExampleFilters();
});

//Register Swagger example providers
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

//Connect Entity Framework Core to SQL Server (DbContext)
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Register background worker for sending recommendation emails
builder.Services.AddHostedService<RecommendationEmailService>();

//Enable in-memory caching for constant data (genres, types, etc.)
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICacheService, MemoryCacheService>();

//Required by QuestPDF to generate PDF reports legally
QuestPDF.Settings.License = LicenseType.Community;

builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalDev", policy =>
       policy.WithOrigins("http://localhost:5173") // porta do Vite
             .AllowAnyHeader()
             .AllowAnyMethod()
             .AllowCredentials());
});



var app = builder.Build();

app.UseCors("LocalDev");
// HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();  //Force HTTPS in production
app.UseAuthentication();    //Enable JWT authentication middleware
app.UseAuthorization();     //Enforce access control for protected routes
app.MapControllers();       //Register all controller endpoints

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    SeedData.Initialize(context);
}

app.Run();

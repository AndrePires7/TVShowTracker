using Microsoft.EntityFrameworkCore;
using TVShowTracker.API.Models;

namespace TVShowTracker.API.Data
{

    //Database context: maps models to database tables
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ShowActor> ShowActors { get; set; }
        public DbSet<FavoriteShow> FavoriteShows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Composite key for ShowActor (prevents duplicate actor show pairs)
            modelBuilder.Entity<ShowActor>()
                .HasKey(sa => new { sa.TVShowId, sa.ActorId });

            modelBuilder.Entity<ShowActor>()
                .HasOne(sa => sa.TVShow)
                .WithMany(s => s.ShowActors)
                .HasForeignKey(sa => sa.TVShowId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShowActor>()
                .HasOne(sa => sa.Actor)
                .WithMany(a => a.ShowActors)
                .HasForeignKey(sa => sa.ActorId)
                .OnDelete(DeleteBehavior.Cascade);

            //Composite key for FavoriteShow (prevents duplicate favorites)
            modelBuilder.Entity<FavoriteShow>()
                .HasKey(fs => new { fs.UserId, fs.TVShowId });

            modelBuilder.Entity<FavoriteShow>()
                .HasOne(fs => fs.User)
                .WithMany(u => u.FavoriteShows)
                .HasForeignKey(fs => fs.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoriteShow>()
                .HasOne(fs => fs.TVShow)
                .WithMany(t => t.FavoriteShows)
                .HasForeignKey(fs => fs.TVShowId)
                .OnDelete(DeleteBehavior.Cascade);

            //Unique index for Episodes per show/season/episode number
            modelBuilder.Entity<Episode>()
                .HasIndex(e => new { e.TVShowId, e.SeasonNumber, e.EpisodeNumber })
                .IsUnique()
                .HasDatabaseName("IX_Episode_TvShow_Season_Episode");

            //Map ReleaseDate to a precise SQL type
            modelBuilder.Entity<Episode>()
                .Property(e => e.ReleaseDate)
                .HasColumnType("datetime2");

            modelBuilder.Entity<TVShow>()
                .Property(t => t.ReleaseDate)
                .HasColumnType("datetime2");

            //Indexes to speed up filters and sorting (genre, type, title)
            modelBuilder.Entity<TVShow>().HasIndex(t => t.Genre).HasDatabaseName("IX_TVShow_Genre");
            modelBuilder.Entity<TVShow>().HasIndex(t => t.Type).HasDatabaseName("IX_TVShow_Type");
            modelBuilder.Entity<TVShow>().HasIndex(t => t.Title).HasDatabaseName("IX_TVShow_Title");

            //Ensure User.Email is unique (prevents duplicate registration)
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_User_Email");
        }
    }
}

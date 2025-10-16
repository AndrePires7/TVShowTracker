TV Show Tracker
A full-stack TV Show Tracker application: .NET API (Entity Framework Core + SQL Server) and React frontend.

1. My approach was to build a small, maintainable, and testable system following SOLID principles.  
2. The backend uses a layered architecture (Controllers → Services → Repositories) with EF Core for data access.  
3. JWT authentication secures API calls; tokens have TTL and expire automatically.  
4. All lists support pagination and sorting by multiple fields for flexible browsing.  
5. Caching (MemoryCache) is used for constant lists (genres/types) with TTL to reduce database load.  
6. A background worker generates personalized TV show recommendations and emails them to users.  
7. Front-end is a responsive React app (Bootstrap) demonstrating registration, login, TV shows, actors, and favorites.  
8. Seed data provides sample shows and actors for quick testing, and CSV/PDF export supports GDPR compliance.  
9. Unit tests cover core services; integration tests were attempted but encountered issues.  
10. Swagger is included to explore and test the API endpoints interactively.
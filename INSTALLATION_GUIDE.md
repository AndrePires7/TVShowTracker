# INSTALLATION GUIDE - TV Show Tracker

## Prerequisites

### Back-end (API)
- .NET 8 SDK
- Visual Studio with ASP.NET support
- SQL Server Express
- SQL Server Management Studio (SSMS) — optional, for exploring the database

### Front-end (React)
- Node.js - includes npm
- VS Code

### Tools
- Git
- Swagger - for API testing

## Backend - Quick Commands
- Open Visual Studio, go to **Package Manager Console**
- Run `Update-Database` to apply migrations and seed the database
- Ensure `appsettings.json` has correct SQL Server connection string

## Frontend - Quick Commands
- Open terminal inside the frontend folder
- `npm install` — Install dependencies
- `npm install axios react-router-dom` — Installs Axios for API requests and React Router DOM
- `npm install bootstrap` — Installs bootstrap for styling
- `npm run dev` — Start development server

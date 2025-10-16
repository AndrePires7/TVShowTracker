# API DOCUMENTATION - TV Show Tracker

## Authentication

### Register
**POST** `/api/Auth/register`
- Body:
```json
{
  "name": "Name",
  "email": "email@example.com",
  "password": "Password123!"
}
```
- Response sample:
```json
{
  "id": 2,
  "name": "Name",
  "email": "email@gmail.com",
  "token": "<JWT_TOKEN>"
}
```

### Login
**POST** `/api/Auth/login`
- Body:
```json
{
  "email": "email@example.com",
  "password": "Password123!"
}
```
- Response sample:
```json
{
  "id": 1,
  "name": "Name",
  "email": "email@gmail.com",
  "token": "<JWT_TOKEN>"
}
```

## TV Shows

### Get all TV shows
**GET** `/api/TVShows`
- Query parameters:
  - `genre` (string) — optional filter
  - `type` (string) — optional filter
  - `search` (string) — search in title/description
  - `sortBy` (string) — Sort by title(default), titledsc , genre, genredsc, type, typedsc, releasedate, realeasedatedsc.
  - `page` (int, default 1)
  - `pageSize` (int, default 10)
- Response sample:
```json
{
  "page": 1,
  "pageSize": 10,
  "totalCount": 11,
  "items": [
    {
      "id": 1,
      "title": "Breaking Bad",
      "genre": "Crime",
      "type": "Drama",
      "releaseDate": "2008-01-20T00:00:00+00:00",
      "imageUrl": "https://..."
    }
}
```

### Get TV show details
**GET** `/api/TVShows/{id}`
- Response includes `episodes` and `featuredActors` arrays.
- Response sample:
```json
{
  "id": 1,
  "title": "Breaking Bad",
  "description": "A chemistry teacher becomes a meth producer.",
  "genre": "Crime",
  "type": "Drama",
  "releaseDate": "2008-01-20T00:00:00Z",
  "imageUrl": "https://...",
  "episodes": [
    {
      "id": 1,
      "title": "Pilot",
      "seasonNumber": 1,
      "episodeNumber": 1,
      "releaseDate": "2008-01-20T00:00:00Z",
      "synopsis": ""
    }
  ],
  "featuredActors": [
    {
      "id": 1,
      "name": "Bryan Cranston",
      "bio": "Walter White",
      "imageUrl": "https://..."
    }
  ]
}
```

## Actors

### Get all actors
**GET** `/api/Actors`
- Query parameters:
  - `sortBy` (string) — name(default), namedsc
  - `page` (int)
  - `pageSize` (int)

- Response sample (paged):
```json
{
  "page": 1,
  "pageSize": 10,
  "totalCount": 32,
  "items": [
    {
      "id": 2,
      "name": "Aaron Paul",
      "bio": "Jesse Pinkman",
      "imageUrl": "https://...",
      "tvShows": [
        {
          "id": 1,
          "title": "Breaking Bad",
          "genre": "Crime",
          "type": "Drama",
          "releaseDate": "2008-01-20T00:00:00+00:00",
          "imageUrl": "https://..."
        }
      ]
    }
}
```

### Get actor details
**GET** `/api/Actors/{id}`
- Response includes `tvShows` array listing shows the actor appears in.
- Response sample:
```json
{
  "id": 1,
  "name": "Bryan Cranston",
  "bio": "Walter White",
  "imageUrl": "https://...",
  "tvShows": [
    {
      "id": 1,
      "title": "Breaking Bad",
      "genre": "Crime",
      "type": "Drama",
      "releaseDate": "2008-01-20T00:00:00+00:00",
      "imageUrl": "https://..."
    }
  ]
}
```

## Favorites (requires JWT)

### Get user's favorites
**GET** `/api/Favorites`
- Returns a list of favorite shows or join objects for the authenticated user.

### Add favorite
**POST** `/api/Favorites`
- Body:
```json
{
  "tvShowId": 1
}
```
- Response: 204

### Remove favorite
**DELETE** `/api/Favorites/{tvShowId}`
- Removes the TV show from the authenticated user favorites.

## Recommendations (requires JWT)

### Get personalized recommendations
**GET** `/api/Recommendations`
- Returns a small list of recommended TV shows based on user's favorite genres.
- Response sample:
```json
[
  {
    "id": 10,
    "title": "Similar Show",
    "genre": "Crime",
    "type": "Drama",
    "releaseDate": "2015-08-28T00:00:00+01:00",
    "imageUrl": "https://..."
  }
]
```

## Exports & GDPR endpoints (requires JWT)

### Export CSV
**GET** `/api/User/me/export/csv`
- Returns `text/csv` file with user data.

### Export PDF
**GET** `/api/User/me/export/pdf`
- Returns `application/pdf` file with user data.

### Delete account (GDPR)
**DELETE** `/api/User/me`
- Deletes the authenticated user's account and related personal data.

## Other endpoints / utilities

### Distinct genres (cached)
**GET** `/api/tvshows/genres`
- Returns list of distinct genres (cached with TTL).

### Distinct types (cached)
**GET** `/api/tvshows/types`
- Returns list of distinct types (cached with TTL).

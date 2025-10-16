import { useEffect, useState } from 'react';
import api from '../api/api';
import { Link } from 'react-router-dom';

export default function ShowList() {
  const [shows, setShows] = useState([]);
  const [page, setPage] = useState(1);
  const [pageSize] = useState(9);
  const [totalCount, setTotalCount] = useState(0);
  const [loading, setLoading] = useState(true);
  const [sortBy, setSortBy] = useState('title'); //default sort

  useEffect(() => {
    fetchShows();
  }, [page, sortBy]);

  async function fetchShows() {
    setLoading(true);
    try {
      const res = await api.get(
        `/api/tvshows?page=${page}&pageSize=${pageSize}&sortBy=${sortBy}`
      );
      setShows(res.data.items);
      setTotalCount(res.data.totalCount);
    } catch (err) {
      console.error('Failed to fetch shows:', err);
    } finally {
      setLoading(false);
    }
  }

  const totalPages = Math.ceil(totalCount / pageSize);

  if (loading) return <div className="text-center mt-4">Loading...</div>;

  return (
    <div className="container my-4">
      <h2 className="mb-4 text-center">TV Shows</h2>

      {/* Sorting Controls */}
      <div className="mb-3 d-flex flex-wrap align-items-center gap-2 justify-content-center">
        <label htmlFor="sortSelect" className="me-2">Sort by:</label>
        <select
          id="sortSelect"
          value={sortBy}
          onChange={(e) => setSortBy(e.target.value)}
          className="form-select w-auto"
        >
          <option value="title">Title (A-Z)</option>
          <option value="titledsc">Title (Z-A)</option>
          <option value="releasedate">Release Date (Oldest)</option>
          <option value="releasedatedsc">Release Date (Newest)</option>
          <option value="genre">Genre (A-Z)</option>
          <option value="genredsc">Genre (Z-A)</option>
          <option value="type">Type (A-Z)</option>
          <option value="typedsc">Type (Z-A)</option>
        </select>
      </div>

      {/* Shows Grid */}
      {shows.length === 0 ? (
        <p className="text-center">No TV shows found.</p>
      ) : (
        <div className="row g-4">
          {shows.map(show => (
            <div key={show.id} className="col-12 col-sm-6 col-md-4">
              <div className="card h-100">
                <Link to={`/shows/${show.id}`}>
                  <img
                    src={show.imageUrl || 'https://via.placeholder.com/300x400?text=No+Image'}
                    className="card-img-top"
                    alt={show.title}
                  />
                </Link>
                <div className="card-body">
                  <h5 className="card-title">
                    <Link to={`/shows/${show.id}`} className="text-decoration-none">
                      {show.title}
                    </Link>
                  </h5>
                  <p className="card-text text-muted">
                    {show.genre} • {show.type}
                  </p>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}

      {/* Pagination */}
      {totalPages > 1 && (
        <div className="d-flex justify-content-center align-items-center gap-2 mt-4 flex-wrap">
          {page > 1 && (
            <button
              onClick={() => setPage(page - 1)}
              className="btn btn-outline-primary btn-sm"
            >
              ← Previous
            </button>
          )}

          <span className="mx-2">
            Page {page} of {totalPages}
          </span>

          {page < totalPages && (
            <button
              onClick={() => setPage(page + 1)}
              className="btn btn-outline-primary btn-sm"
            >
              Next →
            </button>
          )}
        </div>
      )}
    </div>
  );
}

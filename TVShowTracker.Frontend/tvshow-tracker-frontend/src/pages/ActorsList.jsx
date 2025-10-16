import { useEffect, useState } from 'react';
import api from '../api/api';
import { Link } from 'react-router-dom';

export default function ActorsList() {
  const [actors, setActors] = useState([]); //State to store actors
  const [page, setPage] = useState(1); //Current page
  const [pageSize] = useState(9); //Number of actors per page
  const [totalCount, setTotalCount] = useState(0); //Total number of actors
  const [loading, setLoading] = useState(true); //Loading state
  const [sortBy, setSortBy] = useState('name'); //Sorting option, default is name

  //Fetch actors whenever the page or sortBy changes
  useEffect(() => {
    fetchActors();
  }, [page, sortBy]);

  //Fetch actors from API with pagination and sorting
  async function fetchActors() {
    setLoading(true);
    try {
      const res = await api.get(`/api/actors?page=${page}&pageSize=${pageSize}&sortBy=${sortBy}`);
      setActors(res.data.items || res.data); //Supports paged or non-paged response
      setTotalCount(res.data.totalCount ?? res.data.length); //Set total count for pagination
    } catch (err) {
      console.error('Failed to fetch actors:', err);
      setActors([]);
    } finally {
      setLoading(false);
    }
  }

  const totalPages = Math.ceil(totalCount / pageSize); //Calculate total pages

  if (loading) return <div className="text-center mt-4">Loading...</div>;

  return (
    <div className="container my-4">
      <h2 className="mb-3">Actors</h2>

      {/* Sorting Controls */}
      <div className="mb-3 d-flex align-items-center gap-2">
        <label htmlFor="sortSelect" className="me-2">Sort by:</label>
        <select
          id="sortSelect"
          value={sortBy}
          onChange={(e) => setSortBy(e.target.value)}
          className="form-select w-auto"
        >
          <option value="name">Name (A-Z)</option>
          <option value="namedsc">Name (Z-A)</option>
        </select>
      </div>

      {actors.length === 0 ? (
        <p>No actors found.</p>
      ) : (
        <div className="row">
          {actors.map(actor => (
            <div key={actor.id} className="col-12 col-sm-6 col-md-4 mb-3">
              {/* Actor card */}
              <div className="card h-100 shadow-sm">
                {actor.imageUrl && (
                  <Link to={`/actors/${actor.id}`}>
                    <img 
                      src={actor.imageUrl} 
                      className="card-img-top" 
                      alt={actor.name} 
                    />
                  </Link>
                )}
                <div className="card-body">
                  <h5 className="card-title">
                    <Link to={`/actors/${actor.id}`} className="text-decoration-none">
                      {actor.name}
                    </Link>
                  </h5>
                  {actor.bio && <p className="card-text">{actor.bio}</p>}
                </div>
              </div>
            </div>
          ))}
        </div>
      )}

      {/* Pagination controls */}
      {totalPages > 1 && (
        <div className="d-flex justify-content-center align-items-center gap-2 mt-4">
          {page > 1 && (
            <button
              onClick={() => setPage(page - 1)}
              className="btn btn-outline-primary btn-sm"
            >
              ← Previous
            </button>
          )}
          <span>
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

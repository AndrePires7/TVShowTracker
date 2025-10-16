import { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import api from '../api/api';
import 'bootstrap/dist/css/bootstrap.min.css'; // import Bootstrap CSS

export default function ActorDetail() {
  const { id } = useParams();
  const [actor, setActor] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchActor();
  }, [id]);

  // Fetch actor details from API
  async function fetchActor() {
    setLoading(true);
    try {
      const res = await api.get(`/api/actors/${id}`);
      setActor({
        ...res.data,
        tvShows: res.data.tvShows || [] // Ensure tvShows is always an array
      });
    } catch (err) {
      console.error('Failed to fetch actor:', err);
      setActor(null);
    } finally {
      setLoading(false);
    }
  }

  if (loading) return <div className="text-center mt-4">Loading...</div>;
  if (!actor) return <div className="text-center mt-4">Actor not found</div>;

  return (
    <div className="container mt-4">
      {/* Actor Info */}
      <div className="row mb-4 align-items-center">
        {actor.imageUrl && (
          <div className="col-12 col-md-3 text-center mb-3 mb-md-0">
            <img
              src={actor.imageUrl}
              alt={actor.name}
              className="img-fluid rounded shadow-sm"
            />
          </div>
        )}
        <div className={actor.imageUrl ? 'col-12 col-md-9' : 'col-12'}>
          <h1 className="display-5">{actor.name}</h1>
          <div className="card p-3 shadow-sm bg-light">
            <p className="mb-0">{actor.bio || 'No bio available'}</p>
          </div>
        </div>
      </div>

      {/* TV Shows Section */}
      <h3 className="mb-3">TV Shows</h3>
      {actor.tvShows.length > 0 ? (
        <div className="row">
          {actor.tvShows.map(show => (
            <div key={show.id} className="col-12 col-sm-6 col-md-4 mb-3">
              {/* TV Show card */}
              <div className="card h-100 shadow-sm">
                {show.imageUrl && (
                  <Link to={`/shows/${show.id}`}>
                    <img
                      src={show.imageUrl}
                      className="card-img-top"
                      alt={show.title}
                    />
                  </Link>
                )}
                <div className="card-body">
                  <h5 className="card-title">
                    <Link to={`/shows/${show.id}`} className="text-decoration-none">
                      {show.title}
                    </Link>
                  </h5>
                  <p className="card-text text-muted">{show.genre} • {show.type}</p>
                </div>
              </div>
            </div>
          ))}
        </div>
      ) : (
        <p>No TV shows available</p>
      )}
    </div>
  );
}

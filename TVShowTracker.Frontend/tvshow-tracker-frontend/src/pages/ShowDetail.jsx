import { useEffect, useState, useContext } from 'react';
import { useParams, Link } from 'react-router-dom';
import api from '../api/api';
import { AuthContext } from '../contexts/AuthContext';

export default function ShowDetail() {
  const { id } = useParams(); // Get TV show ID from URL
  const { user } = useContext(AuthContext); // Get current logged user
  const [show, setShow] = useState(null);
  const [loading, setLoading] = useState(true);
  const [isFavorite, setIsFavorite] = useState(false);
  const [updatingFavorite, setUpdatingFavorite] = useState(false);

  //Fetch show details whenever ID changes
  useEffect(() => {
    fetchShow();
  }, [id]);

  //Fetch TV show details from API
  async function fetchShow() {
    setLoading(true);
    try {
      const res = await api.get(`/api/tvshows/${id}`);
      setShow({
        ...res.data,
        episodes: res.data.episodes || [],
        featuredActors: res.data.featuredActors || [],
      });

      //If logged in, check if show is already favorite
      if (localStorage.getItem('tv_token')) {
        await checkFavorite(res.data.id);
      } else {
        setIsFavorite(false);
      }
    } catch (err) {
      console.error('Failed to fetch show:', err);
      setShow(null);
      setIsFavorite(false);
    } finally {
      setLoading(false);
    }
  }

  //Check if the current show is in user's favorites
  async function checkFavorite(currentShowId) {
    try {
      const res = await api.get('/api/favorites');
      const favs = res.data || [];
      const favIds = favs.map(f => f.id ?? f.tvShowId ?? (f.tvShow && f.tvShow.id));
      setIsFavorite(favIds.includes(currentShowId));
    } catch (err) {
      console.error('Failed to fetch favorites:', err);
      setIsFavorite(false);
    }
  }

  //Toggle favorite status
  async function toggleFavorite() {
    if (!show || !user) return;
    setUpdatingFavorite(true);

    try {
      if (!isFavorite) {
        await api.post('/api/favorites', { tvShowId: show.id });
        setIsFavorite(true);
      } else {
        await api.delete(`/api/favorites/${show.id}`);
        setIsFavorite(false);
      }
    } catch (err) {
      console.error('Failed to update favorite:', err);
    } finally {
      setUpdatingFavorite(false);
    }
  }

  if (loading) return <div className="text-center mt-4">Loading...</div>;
  if (!show) return <div className="text-center mt-4">Show not found</div>;

  return (
    <div className="container mt-4">
      {/* Show header */}
      <div className="mb-3 d-flex flex-wrap gap-3 align-items-start">
        {/* Show image */}
        <img
          src={show.imageUrl || 'https://via.placeholder.com/300x400?text=No+Image'}
          alt={show.title}
          className="img-fluid rounded"
          style={{ maxWidth: '300px' }}
        />

        <div>
          <h2>{show.title}</h2>
          <p>{show.description}</p>
          <small className="text-muted">
            Genre: {show.genre} | Type: {show.type} | Released: {new Date(show.releaseDate).toLocaleDateString()}
          </small>

          {/* Favorite button (only visible when user is logged in) */}
          {user && (
            <div className="mt-2">
              <button 
                className={`btn ${isFavorite ? 'btn-danger' : 'btn-primary'} mb-2`}
                onClick={toggleFavorite} 
                disabled={updatingFavorite}
              >
                {updatingFavorite ? 'Updating...' : isFavorite ? 'Remove Favorite' : 'Add Favorite'}
              </button>
            </div>
          )}
        </div>
      </div>

      {/* Episodes section */}
      <div className="mb-4">
        <h4>Episodes</h4>
        {show.episodes.length > 0 ? (
          <ul className="list-group">
            {show.episodes.map(ep => (
              <li key={ep.id} className="list-group-item d-flex justify-content-between">
                <span>S{ep.seasonNumber}E{ep.episodeNumber} - {ep.title}</span>
                <small className="text-muted">{new Date(ep.releaseDate).toLocaleDateString()}</small>
              </li>
            ))}
          </ul>
        ) : (
          <p>No episodes available</p>
        )}
      </div>

      {/* Actors section */}
      <div className="mb-4">
        <h4>Actors</h4>
        {show.featuredActors.length > 0 ? (
          <ul className="list-group">
            {show.featuredActors.map(actor => (
              <li key={actor.id} className="list-group-item">
                <Link to={`/actors/${actor.id}`}>{actor.name}</Link>
              </li>
            ))}
          </ul>
        ) : (
          <p>No actors available</p>
        )}
      </div>
    </div>
  );
}

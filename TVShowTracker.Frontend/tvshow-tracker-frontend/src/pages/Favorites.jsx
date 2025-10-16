import { useEffect, useState } from 'react';
import api from '../api/api';
import { Link } from 'react-router-dom';

const Favorites = () => {
  const [favorites, setFavorites] = useState([]);
  const [recommendations, setRecommendations] = useState([]);

  //Fetch user's favorite TV shows
  const fetchFavorites = async () => {
    try {
      const res = await api.get('/api/favorites');
      setFavorites(res.data || []);
    } catch (err) {
      console.error(err);
      setFavorites([]);
    }
  };

  //Fetch personalized recommendations
  const fetchRecommendations = async () => {
    try {
      const res = await api.get('/api/recommendations');
      setRecommendations(res.data || []);
    } catch (err) {
      console.error(err);
      setRecommendations([]);
    }
  };

  useEffect(() => {
    fetchFavorites();
    fetchRecommendations();
  }, []);

  //Add or remove a favorite
  const toggleFavorite = async (tvShowId) => {
    try {
      const isFavorite = favorites.some(f => 
        f.id === tvShowId || f.tvShowId === tvShowId || (f.tvShow && f.tvShow.id === tvShowId)
      );

      if (isFavorite) {
        await api.delete(`/api/favorites/${tvShowId}`);
      } else {
        await api.post('/api/favorites', { tvShowId });
      }

      //Refresh lists after change
      await fetchFavorites();
      await fetchRecommendations();
    } catch (err) {
      console.error('Failed to toggle favorite', err);
    }
  };

  //Helper function to get TV show info safely
  const getTvShowInfo = (tv) => {
    const id = tv.id ?? tv.tvShowId ?? (tv.tvShow && tv.tvShow.id);
    const title = tv.title ?? (tv.tvShow && tv.tvShow.title) ?? 'Untitled';
    const imageUrl = tv.imageUrl ?? (tv.tvShow && tv.tvShow.imageUrl) ?? 'https://via.placeholder.com/300x400?text=No+Image';
    return { id, title, imageUrl };
  };

  //Function to render a list of shows as responsive cards
  const renderCardGrid = (shows, isFavoriteSection = true) => (
    <div className="row g-3">
      {shows.map(tv => {
        const { id, title, imageUrl } = getTvShowInfo(tv);
        return (
          <div key={id} className="col-12 col-sm-6 col-md-4">
            <div className="card h-100">
              <Link to={`/shows/${id}`}>
                <img src={imageUrl} className="card-img-top" alt={title} />
              </Link>
              <div className="card-body d-flex flex-column">
                <h5 className="card-title">{title}</h5>
                <button
                  onClick={() => toggleFavorite(id)}
                  className={`btn mt-auto ${isFavoriteSection ? 'btn-danger' : 'btn-success'}`}
                >
                  {isFavoriteSection ? 'Remove' : 'Add'}
                </button>
              </div>
            </div>
          </div>
        );
      })}
    </div>
  );

  return (
    <div className="container py-4">
      {/* Favorites Section */}
      <h2 className="mb-3">My Favorite TV Shows</h2>
      {favorites.length === 0 ? <p>You have no favorites yet.</p> : renderCardGrid(favorites, true)}

      {/* Recommendations Section */}
      <h2 className="mt-5 mb-3">Recommended For You</h2>
      {recommendations.length === 0 ? <p>No recommendations yet.</p> : renderCardGrid(recommendations, false)}
    </div>
  );
};

export default Favorites;

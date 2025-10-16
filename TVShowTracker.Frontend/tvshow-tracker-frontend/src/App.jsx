//Main entry component that handles routing, layout, and user session UI.
import { useContext } from 'react';
import { Routes, Route, Link, Navigate } from 'react-router-dom';
import Login from './pages/Login';
import Register from './pages/Register';
import ShowsList from './pages/ShowsList';
import ShowDetail from './pages/ShowDetail';
import ActorsList from './pages/ActorsList';
import ActorDetail from './pages/ActorDetail';
import Favorites from './pages/Favorites';
import { AuthContext } from './contexts/AuthContext';
import ProtectedRoute from './components/ProtectedRoute';
import api from './api/api';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js'; 

export default function App() {
  const { user, logout } = useContext(AuthContext);

  //Helper function to download CSV or PDF blobs
  function downloadBlob(data, filename, mime) {
    const blob = new Blob([data], { type: mime });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = filename;
    a.click();
    window.URL.revokeObjectURL(url);
  }

  //Export user data as CSV
  async function exportCsv() {
    try {
      const res = await api.get('/api/User/me/export/csv', { responseType: 'blob' });
      downloadBlob(res.data, 'my-data.csv', 'text/csv');
    } catch (err) {
      console.error('Export CSV failed:', err);
      alert('Failed to export CSV.');
    }
  }

  //Export user data as PDF
  async function exportPdf() {
    try {
      const res = await api.get('/api/User/me/export/pdf', { responseType: 'blob' });
      downloadBlob(res.data, 'my-data.pdf', 'application/pdf');
    } catch (err) {
      console.error('Export PDF failed:', err);
      alert('Failed to export PDF.');
    }
  }

  //Delete user account
  async function deleteAccount() {
    if (!window.confirm('Are you sure you want to delete your account? This action cannot be undone.')) return;
    try {
      await api.delete('/api/User/me');
      alert('Account deleted successfully.');
      logout();
    } catch (err) {
      console.error('Delete failed:', err);
      alert('Account deletion failed.');
    }
  }

  return (
    <div className="min-vh-100 d-flex flex-column">
      {/* NAVBAR */}
      <header className="navbar navbar-expand-lg navbar-light bg-light border-bottom shadow-sm px-3">
        <div className="container-fluid">
          {/* Left side links */}
          <Link to="/" className="navbar-brand fw-bold">TV Show Tracker</Link>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarNav"
          >
            <span className="navbar-toggler-icon"></span>
          </button>

          {/* Collapsible menu for mobile */}
          <div className="collapse navbar-collapse" id="navbarNav">
            <ul className="navbar-nav me-auto">
              <li className="nav-item">
                <Link to="/" className="nav-link">Home</Link>
              </li>
              <li className="nav-item">
                <Link to="/actors" className="nav-link">Actors</Link>
              </li>
              {user && (
                <li className="nav-item">
                  <Link to="/favorites" className="nav-link">Favorites</Link>
                </li>
              )}
            </ul>

            {/* Right side (user actions) */}
            <div className="d-flex align-items-center gap-2">
              {user ? (
                <>
                  <span className="text-muted small me-2">
                    Welcome, <b>{user.name}</b>
                  </span>
                  <button onClick={exportCsv} className="btn btn-outline-secondary btn-sm">CSV</button>
                  <button onClick={exportPdf} className="btn btn-outline-secondary btn-sm">PDF</button>
                  <button onClick={deleteAccount} className="btn btn-outline-danger btn-sm">Delete</button>
                  <button onClick={logout} className="btn btn-primary btn-sm">Logout</button>
                </>
              ) : (
                <>
                  <Link to="/login" className="btn btn-outline-primary btn-sm">Login</Link>
                  <Link to="/register" className="btn btn-primary btn-sm">Register</Link>
                </>
              )}
            </div>
          </div>
        </div>
      </header>

      {/* --- MAIN CONTENT --- */}
      <main className="container flex-grow-1 py-4">
        <Routes>
          <Route path="/" element={<ShowsList />} />
          <Route path="/shows/:id" element={<ShowDetail />} />
          <Route path="/actors" element={<ActorsList />} />
          <Route path="/actors/:id" element={<ActorDetail />} />
          <Route path="/favorites" element={<ProtectedRoute><Favorites /></ProtectedRoute>} />

          {/* Redirect if already logged in */}
          <Route path="/login" element={!user ? <Login /> : <Navigate to="/" />} />
          <Route path="/register" element={!user ? <Register /> : <Navigate to="/" />} />
        </Routes>
      </main>

      {/* --- FOOTER --- */}
      <footer className="bg-light text-center py-3 mt-auto border-top small text-muted">
        © {new Date().getFullYear()} TV Show Tracker — Developed by André Pires
      </footer>
    </div>
  );
}

import { useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../contexts/AuthContext';

export default function Login() {
  const { login } = useContext(AuthContext);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [err, setErr] = useState('');
  const navigate = useNavigate();

  // Handles form submission
  async function handleSubmit(e) {
    e.preventDefault();
    try {
      await login(email, password); // call login from AuthContext
      navigate('/'); // redirect to home after successful login
    } catch (error) {
      setErr(error.response?.data?.message || 'Login failed'); // display error message
    }
  }

  return (
    <div className="container mt-5" style={{ maxWidth: '420px' }}>
      <h2 className="mb-4 text-center">Login</h2>

      {err && (
        <div className="alert alert-danger" role="alert">
          {err}
        </div>
      )}

      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label className="form-label">Email</label>
          <input
            type="email"
            className="form-control"
            value={email}
            onChange={e => setEmail(e.target.value)}
            placeholder="Enter your email"
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">Password</label>
          <input
            type="password"
            className="form-control"
            value={password}
            onChange={e => setPassword(e.target.value)}
            placeholder="Enter your password"
            required
          />
        </div>

        <button type="submit" className="btn btn-primary w-100">
          Login
        </button>
      </form>
    </div>
  );
}

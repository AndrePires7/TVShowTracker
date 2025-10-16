import { useState } from 'react';
import api from '../api/api';
import { useNavigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css'; // Import Bootstrap CSS

export default function Register() {
  const [name, setName] = useState('');        // User name state
  const [email, setEmail] = useState('');      // User email state
  const [password, setPassword] = useState('');// User password state
  const [err, setErr] = useState('');          // Error message state
  const navigate = useNavigate();              // React Router navigation

  // Handles form submission
  async function handleSubmit(e) {
    e.preventDefault();
    try {
      // Call API to register user
      await api.post('/api/auth/register', { name, email, password });
      navigate('/login'); // Redirect to login after successful registration
    } catch (error) {
      if (error.response?.data?.errors) {
        // Combine all validation errors into a single string
        const messages = Object.values(error.response.data.errors)
          .flat() // caso cada campo tenha mais de um erro
          .join(' • ');
        setErr(messages);
      } else {
        setErr(error.response?.data?.message || 'Registration failed');
      }
    }
  }

  return (
    <div className="d-flex justify-content-center align-items-center vh-100">
      <form 
        onSubmit={handleSubmit} 
        className="border p-4 rounded shadow-sm bg-light" 
        style={{ width: '100%', maxWidth: '400px' }}
      >
        <h2 className="mb-4 text-center">Register</h2>

        {/* Error message */}
        {err && <div className="alert alert-danger">{err}</div>}

        {/* Name input */}
        <div className="mb-3">
          <label className="form-label">Name</label>
          <input 
            type="text" 
            className="form-control" 
            value={name} 
            onChange={e => setName(e.target.value)} 
            required
          />
        </div>

        {/* Email input */}
        <div className="mb-3">
          <label className="form-label">Email</label>
          <input 
            type="email" 
            className="form-control" 
            value={email} 
            onChange={e => setEmail(e.target.value)} 
            required
          />
        </div>

        {/* Password input */}
        <div className="mb-3">
          <label className="form-label">Password</label>
          <input 
            type="password" 
            className="form-control" 
            value={password} 
            onChange={e => setPassword(e.target.value)} 
            required
          />
        </div>

        {/* Submit button */}
        <button type="submit" className="btn btn-primary w-100">
          Register
        </button>
      </form>
    </div>
  );
}

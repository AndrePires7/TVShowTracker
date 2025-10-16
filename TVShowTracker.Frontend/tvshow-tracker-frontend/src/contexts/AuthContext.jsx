import { createContext, useState, useEffect } from 'react';
import api from '../api/api';

//AuthContext provides authentication state and actions to the app
const AuthContext = createContext();

function AuthProvider({ children }) {
  const [user, setUser] = useState(null); //Stores logged-in user's info

  //On app load, check if token and user name exist in localStorage
  useEffect(() => {
    const token = localStorage.getItem('tv_token');
    const name = localStorage.getItem('tv_name');
    if (token && name) setUser({ name }); //Set user if token is valid
  }, []);

  //Handles login by calling API and storing token + name
  async function login(email, password) {
    try {
      const res = await api.post('/api/auth/login', { email, password });
      const { token, name } = res.data;

      localStorage.setItem('tv_token', token);
      localStorage.setItem('tv_name', name || '');

      setUser({ name });
    } catch (error) {
      console.error('Login failed:', error);
      throw error;
    }
  }

  //Clears user session
  function logout() {
    localStorage.removeItem('tv_token');
    localStorage.removeItem('tv_name');
    setUser(null);
  }

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
}

export { AuthContext, AuthProvider };
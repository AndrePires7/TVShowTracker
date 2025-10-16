//Axios instance used for all API requests in the app.
//Automatically attaches the JWT token (tv_token) to each request and redirects to login if the token is invalid or expired.
import axios from 'axios';
//Base API URL comes from .env
const API_URL = import.meta.env.VITE_API_URL;

//Create a reusable Axios instance
const api = axios.create({
  baseURL: API_URL,
});

//Add JWT token to every request (key: tv_token)
api.interceptors.request.use(config => {
  const token = localStorage.getItem('tv_token');
  if (token && config.headers) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

//Response interceptor: if backend returns 401, clear token and redirect to login
api.interceptors.response.use(
  res => res,
  err => {
    if (err.response && err.response.status === 401) {
      //Remove token and redirect to login page
      localStorage.removeItem('tv_token');
      localStorage.removeItem('tv_name');

      window.location.href = '/login';
    }
    return Promise.reject(err);
  }
);

export default api;

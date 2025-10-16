//This component protects routes that require authentication.
//If the user is not logged in, it redirects them to the login page.
import { useContext } from 'react';
import { Navigate } from 'react-router-dom';
import { AuthContext } from '../contexts/AuthContext';

export default function ProtectedRoute({ children }) {
   //Get current logged-in user from context
  const { user } = useContext(AuthContext);

  //If user is not logged in, redirect to /login
  if (!user) return <Navigate to="/login" replace />;

  //If user is logged in, render the child component
  return children;
}

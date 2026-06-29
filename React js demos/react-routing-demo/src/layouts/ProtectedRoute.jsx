import { Navigate, useLocation } from "react-router-dom";
export function ProtectedRoute({ children, allowedRoles }) {
  const location = useLocation();
  const user = JSON.parse(localStorage.getItem("user"));

  if (!user || !user.isAuthenticated) {
    return <Navigate to="/login" replace state={{ from: location }} />;
  }
  if (allowedRoles && !allowedRoles.includes(user.role)) {
    return (
      <div className="page">
        <h2> Access Denied.</h2>
        <p> you do not have permission to access this page.</p>
      </div>
    );
  }
  return children;
}

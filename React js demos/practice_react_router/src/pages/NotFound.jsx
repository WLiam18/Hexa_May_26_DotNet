import { Link } from "react-router-dom";

function NotFound() {
  return (
    <div className="page">
      <h2>404 - Page Not Found</h2>
      <Link to="/" className="button-link">Go Home</Link>
    </div>
  );
}

export default NotFound;
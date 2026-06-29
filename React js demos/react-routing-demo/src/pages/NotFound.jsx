import { Link } from "react-router-dom";
export function NotFound() {
  return (
    <div className="page not-found">
      <h2>404 - Page Not Found</h2>
      <p>The page you are looking for does not exist.</p>

      <Link to="/" className="button-link">
        Go to Home
      </Link>
    </div>
  );
}

export function Header({ loggedInUser, onLogout }) {
  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-primary shadow-sm">
      <div className="container">
        <div>
          <span className="navbar-brand fw-bold">BookShelf E-Commerce</span>
          <div className="text-white-50 small">
            Welcome, {loggedInUser.name}
            <span className="badge text-bg-light text-primary ms-2">
              {loggedInUser.role}
            </span>
          </div>
        </div>

        <button type="button" className="btn btn-light" onClick={onLogout}>
          Logout
        </button>
      </div>
    </nav>
  );
}

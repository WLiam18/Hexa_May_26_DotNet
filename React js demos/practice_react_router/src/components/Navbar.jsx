import { NavLink, useNavigate } from "react-router-dom";

function Navbar() {
  const navigate = useNavigate();
  const user = JSON.parse(localStorage.getItem("user"));

  const getNavClass = ({ isActive }) => 
    isActive ? "nav-link active" : "nav-link";

  const handleLogout = () => {
    localStorage.removeItem("user");
    navigate("/login");
  };

  return (
    <nav className="navbar">
      <h2>My App</h2>
      <div className="nav-links">
        <NavLink to="/" className={getNavClass} end>Home</NavLink>
        <NavLink to="/about" className={getNavClass}>About</NavLink>
        <NavLink to="/dashboard" className={getNavClass}>Dashboard</NavLink>

        {user && (
          <NavLink to="/settings" className={getNavClass}>Settings</NavLink>
        )}

        {!user ? (
          <NavLink to="/login" className={getNavClass}>Login</NavLink>
        ) : (
          <button onClick={handleLogout} className="logout-btn">Logout</button>
        )}
      </div>
    </nav>
  );
}

export default Navbar;
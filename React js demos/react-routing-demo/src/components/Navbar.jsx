import { NavLink } from "react-router-dom";

export function Navbar() {
  const user = JSON.parse(localStorage.getItem("user"));

  function getNavClass({ isActive }) {
    return isActive ? "nav-link active-link" : "nav-link";
  }

  return (
    <nav className="navbar">
      <h2 className="logo">Product Gallery</h2>

      <div className="nav-links">
        <NavLink to="/" className={getNavClass} end>
          Home
        </NavLink>

        <NavLink to="/products" className={getNavClass}>
          Products
        </NavLink>

        <NavLink to="/about" className={getNavClass}>
          About
        </NavLink>

        <NavLink to="/contact" className={getNavClass}>
          Contact
        </NavLink>

        <NavLink to="/cart" className={getNavClass}>
          Cart
        </NavLink>

        <NavLink to="/dashboard" className={getNavClass}>
          Dashboard
        </NavLink>

        <NavLink to="/admin" className={getNavClass}>
          Admin
        </NavLink>

        {!user && (
          <NavLink to="/login" className={getNavClass}>
            Login
          </NavLink>
        )}
      </div>
    </nav>
  );
}

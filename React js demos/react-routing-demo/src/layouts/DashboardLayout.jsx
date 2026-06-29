import { Link, Outlet, useNavigate } from "react-router-dom";

export function DashboardLayout() {
  const navigate = useNavigate();
  const user = JSON.parse(localStorage.getItem("user"));

  function handleLogout() {
    localStorage.removeItem("user");
    navigate("/login");
  }
  return (
    <div className="page">
      <h2>Dashboard</h2>
      <p>
        {" "}
        Welcome , <strong>{user?.username}</strong>({user?.role})
      </p>

      <div className="dashboard=menu">
        <Link to="/dashboard" className="button-link">
          Dashboard Home
        </Link>

        <Link to="/dashboard/orders" className="button-link">
          Orders
        </Link>
        <button onClick={handleLogout}>Logout</button>
      </div>
      <div className="dashboard-content">
        <Outlet />
      </div>
    </div>
  );
}

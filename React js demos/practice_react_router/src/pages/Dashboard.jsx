function Dashboard() {
  const user = JSON.parse(localStorage.getItem("user"));

  return (
    <div>
      <h2>Dashboard</h2>
      <p>Welcome back, {user?.username || "User"}!</p>
      <p>This page is protected. You can only see it when logged in.</p>
    </div>
  );
}

export default Dashboard;
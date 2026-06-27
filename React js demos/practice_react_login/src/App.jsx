import { useState } from "react";
import Login from "./components/auth/Login";
import Dashboard from "./components/dashboard/Dashboard";
import "./App.css";

function App() {
  const [user, setUser] = useState(null);

  const handleLogin = (username, password) => {
    const validUsers = [
      { username: "user", password: "user123", role: "user" },
      { username: "admin", password: "admin123", role: "admin" },
    ];

    const found = validUsers.find(
      (u) => u.username === username && u.password === password
    );

    if (found) {
      setUser(found);
    } else {
      alert("Invalid credentials");
    }
  };

  const handleLogout = () => {
    setUser(null);
  };

  if (!user) {
    return <Login onLogin={handleLogin} />;
  }

  return <Dashboard user={user} onLogout={handleLogout} />;
}

export default App;
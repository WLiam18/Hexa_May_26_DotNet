import { useContext } from "react";
import { UserContext } from "../context/UserContext";
import { ThemeContext } from "../context/ThemeContext";
export function Header() {
  const { user } = useContext(UserContext);
  const { theme, toggleTheme } = useContext(ThemeContext);
  return (
    <div className={`card ${theme}`}>
      <h2>Header</h2>

      {user ? (
        <p>
          Welcome, <strong>{user.name}</strong>
        </p>
      ) : (
        <p>Please login to continue</p>
      )}
    </div>
  );
}

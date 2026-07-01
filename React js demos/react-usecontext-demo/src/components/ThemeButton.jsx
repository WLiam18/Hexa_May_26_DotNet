import { useContext } from "react";
import { ThemeContext } from "../context/ThemeContext";

export function ThemeButton() {
  const { theme, toggleTheme } = useContext(ThemeContext);

  return (
    <div className={`card ${theme}`}>
      <h2>Theme settings</h2>
      <p>Current Theme: {theme}</p>
      <button onClick={toggleTheme}>Toggle Theme</button>
    </div>
  );
}

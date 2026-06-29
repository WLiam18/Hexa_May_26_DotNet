import { useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "./assets/vite.svg";
import heroImg from "./assets/hero.png";
import "./App.css";
import { EmployeeTable } from "./compoenents/EmployeeTable";
import { EmployeeFetchDemo } from "./pages/EmployeeFetchDemo";

function App() {
  const [count, setCount] = useState(0);

  return (
    <>
      <div className="app">
        <EmployeeFetchDemo />
      </div>
    </>
  );
}

export default App;

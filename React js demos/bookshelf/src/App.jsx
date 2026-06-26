import { useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "./assets/vite.svg";
import heroImg from "./assets/hero.png";
import "./App.css";
import Welcome from "./Welcome";
function App() {
  const [count, setCount] = useState(0);
  const mybooks = ["Biology", "Global perspective", "Chemistry"];
  return (
    <>
      <Welcome name="Fransy" />
      <Welcome name="Peter" books={mybooks} />
      <h1 className="title">Hello, BookShelf</h1>;
    </>
  );
}

export default App;

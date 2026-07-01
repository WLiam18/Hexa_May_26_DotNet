import { useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "./assets/vite.svg";
import heroImg from "./assets/hero.png";
import "./App.css";
import { UserProvider } from "./context/UserContext";
// import { UserContext } from "./context/UserContext";
import { BasicProfile } from "./components/BasicProfile";
import { Header } from "./components/Header";
import { LoginPanel } from "./components/LoginPanel";
import { Dashboard } from "./components/Dashboard";
import { ThemeProvider } from "./context/ThemeContext";
import { ThemeButton } from "./components/ThemeButton";
function App() {
  // const loggedInUser = {
  //   name: "Geetha",
  //   role: "React Developer",
  //   email: "geetha.training@gmail.com",
  // };
  return (
    <>
      {/* <UserContext.Provider value={loggedInUser}> */}
      <UserProvider>
        <ThemeProvider>
          <div className="app-container">
            <h1> React UseContext Hook Demo</h1>
            <ThemeButton />
            <Header />
            <LoginPanel />
            <Dashboard />
            {/* <BasicProfile /> */}
          </div>
        </ThemeProvider>
      </UserProvider>
      {/* </UserContext.Provider> */}
    </>
  );
}

export default App;

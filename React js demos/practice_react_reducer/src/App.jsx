import { CartProvider } from "./context/CartContext";
import { Navbar } from "./components/Navbar";
import { ProductList } from "./components/ProductList";
import { Cart } from "./components/Cart";
import "./App.css";

function App() {
  return (
    <CartProvider>
      <div className="app">
        <Navbar />
        <div className="container">
          <ProductList />
          <Cart />
        </div>
      </div>
    </CartProvider>
  );
}

export default App;
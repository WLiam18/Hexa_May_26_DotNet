import { useContext } from "react";
import { CartContext } from "../context/CartContext";

export function Navbar() {
  const { totalItems } = useContext(CartContext);

  return (
    <nav>
      <h2>Store</h2>
      <span>Cart: {totalItems}</span>
    </nav>
  );
}
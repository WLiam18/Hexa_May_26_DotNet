import { useCart } from "../context/CartContext";

export function Navbar() {
  const { totalItems } = useCart();

  return (
    <nav>
      <h2>My Store</h2>
      <span>{totalItems}</span>
    </nav>
  );
}
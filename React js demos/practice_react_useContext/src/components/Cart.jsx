import { useContext } from "react";
import { CartContext } from "../context/CartContext";

export function Cart() {
  const { cart, removeFromCart, clearCart, totalPrice } = useContext(CartContext);

  if (cart.length === 0) {
    return <p>Cart is empty</p>;
  }

  return (
    <div className="cart">
      <h2>Cart</h2>
      <button className = "clear-btn"onClick={clearCart}>Clear</button>
      {cart.map((item) => (
        <div key={item.id} className="cart-item">
          <span>
            {item.name} x{item.qty}
          </span>
          <span>{item.price * item.qty}</span>
          <button onClick={() => removeFromCart(item.id)}>X</button>
        </div>
      ))}
      <div className="cart-total">
        <strong>Total: {totalPrice}</strong>
      </div>
    </div>
  );
}
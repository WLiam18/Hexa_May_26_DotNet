import { useContext } from "react";
import { CartContext } from "../context/CartContext";

export function CartTotal() {
  const { totalItems, totalPrice } = useContext(CartContext);

  return (
    <div className="cart-total">
      <p>Total Items: {totalItems}</p>
      <p>Total Price: {totalPrice}</p>
    </div>
  );
}

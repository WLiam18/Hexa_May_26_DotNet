import { useCart } from "../context/CartContext";

export function Cart() {
  const {
    items,
    totalItems,
    totalPrice,
    removeItem,
    increaseQty,
    decreaseQty,
    clearCart,
  } = useCart();

  if (items.length === 0) {
    return (
      <div className="cart">
        <h2>Cart</h2>
        <p>Cart is empty</p>
      </div>
    );
  }

  return (
    <div className="cart">
      <h2>Cart ({totalItems} items)</h2>
      <button className="clear-btn" onClick={clearCart}>Clear</button>

      {items.map((item) => (
        <div key={item.id} className="cart-item">
          <span>
            {item.name} x{item.qty}
          </span>
          <span>{item.price * item.qty}</span>
          <div>
            <button onClick={() => decreaseQty(item.id)}>-</button>
            <button onClick={() => increaseQty(item.id)}>+</button>
            <button className="remove-btn" onClick={() => removeItem(item.id)}>
              X
            </button>
          </div>
        </div>
      ))}

      <div className="cart-total">Total: {totalPrice}</div>
    </div>
  );
}
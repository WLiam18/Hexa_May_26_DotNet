import { useCart } from "../context/CartContext";

export function CartSummary() {
  const {
    items,
    totalItems,
    totalAmount,
    removeItem,
    increaseQuantity,
    decreaseQuantity,
    clearCart,
  } = useCart();

  return (
    <div className="card">
      <h2> Cart Summary</h2>

      {items.length === 0 ? (
        <p> your cart is Empty.</p>
      ) : (
        <>
          <p>
            <strong>Total Items:</strong> {totalItems}
          </p>
          <p>
            <strong>Total Amount:</strong> ₹{totalAmount}
          </p>

          {items.map((item) => (
            <div className="cart-item" key={item.id}>
              <h3>{item.name}</h3>
              <p>Price: ₹{item.price}</p>
              <p>Quantity: {item.quantity}</p>
              <p>Subtotal: ₹{item.price * item.quantity}</p>

              <button onClick={() => increaseQuantity(item.id)}>+</button>
              <button onClick={() => decreaseQuantity(item.id)}>-</button>
              <button onClick={() => removeItem(item.id)}>Remove</button>
            </div>
          ))}

          <button onClick={clearCart}>Clear Cart</button>
        </>
      )}
    </div>
  );
}

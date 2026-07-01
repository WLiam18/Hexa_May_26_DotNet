import { createContext, useReducer, useContext } from "react";

const CartContext = createContext();

const initialState = {
  items: [],
};

function cartReducer(state, action) {
  switch (action.type) {
    case "ADD": {
      const existing = state.items.find((i) => i.id === action.payload.id);
      if (existing) {
        return {
          ...state,
          items: state.items.map((i) =>
            i.id === action.payload.id
              ? { ...i, qty: i.qty + 1 }
              : i
          ),
        };
      }
      return {
        ...state,
        items: [...state.items, { ...action.payload, qty: 1 }],
      };
    }

    case "REMOVE": {
      return {
        ...state,
        items: state.items.filter((i) => i.id !== action.payload),
      };
    }

    case "INCREASE": {
      return {
        ...state,
        items: state.items.map((i) =>
          i.id === action.payload
            ? { ...i, qty: i.qty + 1 }
            : i
        ),
      };
    }

    case "DECREASE": {
      const updated = state.items
        .map((i) =>
          i.id === action.payload
            ? { ...i, qty: i.qty - 1 }
            : i
        )
        .filter((i) => i.qty > 0);
      return { ...state, items: updated };
    }

    case "CLEAR": {
      return { ...state, items: [] };
    }

    default:
      return state;
  }
}

export function CartProvider({ children }) {
  const [state, dispatch] = useReducer(cartReducer, initialState);

  const totalItems = state.items.reduce((sum, i) => sum + i.qty, 0);
  const totalPrice = state.items.reduce((sum, i) => sum + i.price * i.qty, 0);

  function addItem(product) {
    dispatch({ type: "ADD", payload: product });
  }

  function removeItem(id) {
    dispatch({ type: "REMOVE", payload: id });
  }

  function increaseQty(id) {
    dispatch({ type: "INCREASE", payload: id });
  }

  function decreaseQty(id) {
    dispatch({ type: "DECREASE", payload: id });
  }

  function clearCart() {
    dispatch({ type: "CLEAR" });
  }

  return (
    <CartContext.Provider
      value={{
        items: state.items,
        totalItems,
        totalPrice,
        addItem,
        removeItem,
        increaseQty,
        decreaseQty,
        clearCart,
      }}
    >
      {children}
    </CartContext.Provider>
  );
}

export function useCart() {
  const context = useContext(CartContext);
  if (!context) throw new Error("useCart must be used inside CartProvider");
  return context;
}
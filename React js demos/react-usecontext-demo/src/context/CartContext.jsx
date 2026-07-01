import { createContext, useContext, useReducer } from "react";

const CartContext = createContext();

const initialCartState = {
  items: [],
};

function cartReducer(state, action) {
  switch (action.type) {
    case "ADD_ITEM": {
      const existingItem = state.items.find(
        (item) => item.id === action.payload.id,
      );

      if (existingItem) {
        const updatedItems = state.items.map((item) =>
          item.id === action.payload.id
            ? {
                ...item,
                quantity: item.quantity + 1,
              }
            : item,
        );

        return {
          ...state,
          items: updatedItems,
        };
      }

      return {
        ...state,
        items: [
          ...state.items,
          {
            ...action.payload,
            quantity: 1,
          },
        ],
      };
    }

    case "REMOVE_ITEM": {
      const updatedItems = state.items.filter(
        (item) => item.id !== action.payload,
      );

      return {
        ...state,
        items: updatedItems,
      };
    }

    case "INCREASE_QUANTITY": {
      const updatedItems = state.items.map((item) =>
        item.id === action.payload
          ? {
              ...item,
              quantity: item.quantity + 1,
            }
          : item,
      );

      return {
        ...state,
        items: updatedItems,
      };
    }

    case "DECREASE_QUANTITY": {
      const updatedItems = state.items
        .map((item) =>
          item.id === action.payload
            ? {
                ...item,
                quantity: item.quantity - 1,
              }
            : item,
        )
        .filter((item) => item.quantity > 0);

      return {
        ...state,
        items: updatedItems,
      };
    }

    case "CLEAR_CART": {
      return initialCartState;
    }

    default:
      return state;
  }
}

export function CartProvider({ children }) {
  const [cartState, dispatch] = useReducer(cartReducer, initialCartState);

  function addItem(product) {
    dispatch({
      type: "ADD_ITEM",
      payload: product,
    });
  }

  function removeItem(productId) {
    dispatch({
      type: "REMOVE_ITEM",
      payload: productId,
    });
  }

  function increaseQuantity(productId) {
    dispatch({
      type: "INCREASE_QUANTITY",
      payload: productId,
    });
  }

  function decreaseQuantity(productId) {
    dispatch({
      type: "DECREASE_QUANTITY",
      payload: productId,
    });
  }

  function clearCart() {
    dispatch({
      type: "CLEAR_CART",
    });
  }

  const totalItems = cartState.items.reduce(
    (total, item) => total + item.quantity,
    0,
  );

  const totalAmount = cartState.items.reduce(
    (total, item) => total + item.price * item.quantity,
    0,
  );

  return (
    <CartContext.Provider
      value={{
        items: cartState.items,
        totalItems,
        totalAmount,
        addItem,
        removeItem,
        increaseQuantity,
        decreaseQuantity,
        clearCart,
      }}
    >
      {children}
    </CartContext.Provider>
  );
}

export function useCart() {
  const context = useContext(CartContext);

  if (!context) {
    throw new Error("useCart must be used inside CartProvider");
  }

  return context;
}

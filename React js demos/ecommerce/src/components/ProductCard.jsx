import React from "react";

export function ProductCard({ product }) {
  //implementing Destructuring
  const { name, price, category, stock, rating, seller, image, description } =
    product;
  return (
    <>
      <div className="product-card">
        <img src={image} alt={name} />
        <div className="product-content">
          <p className="category">{category}</p>
          <h2>{name}</h2>
          <p>{description}</p>
        </div>

        <div className="product-info">
          <span>Seller : {seller}</span>
          <span>Rating : ⭐ {rating}</span>
        </div>

        <h3>Price: {price}</h3>

        {stock === 0 ? (
          <p className="out-stock">Out of Stock</p>
        ) : stock <= 5 ? (
          <p className="low-stock"> Only {stock} left </p>
        ) : (
          <p className="in-stock">In Stock :{stock}</p>
        )}

        <button disabled={stock === 0}>
          {stock === 0 ? "Unavailable" : "Add to Cart"}
        </button>
      </div>
    </>
  );
}

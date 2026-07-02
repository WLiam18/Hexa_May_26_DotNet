import React, { useState } from "react";
import { ProductForm } from "./ProductForm";

export function ProductCard({
  product,
  loggedInUser,
  onUpdateProduct,
  onRemoveLowRatedProduct,
  isAdmin,
  isSeller,
}) {
  const [isEditiing, setIsEditing] = useState(false);
  //implementing Destructuring
  const { name, price, category, stock, rating, seller, image, description } =
    product;

  function handleUpdateProduct(updatedProduct) {
    onUpdateProduct({
      ...updatedProduct,
      id: product.id,
    });
    setIsEditing(false);
    if (isEditiing) {
      retrun(
        <ProductForm
          mode="edit"
          productToEdit={product}
          onSubmitProduct={handleUpdateProduct}
          onCancel={() => setIsEditing(false)}
          loggedInUser={loggedInUser}
        />,
      );
    }
  }
  return (
    <>
      <article className="card h-100 shadow-sm border-0 product-card-hover">
        <img src={image} alt={name} className="card-img-top product-img" />
        <div className="card-body d-flex flex-column">
          <span className="badge text-bg-info align-self-start mb-2">
            {category}
          </span>

          <h2 className=" h5 card-title text-primary">{name}</h2>
          <p className="card-text text-muted">{description}</p>
          <p className="mb-1">
            <strong>Selelr:</strong> {seller}
          </p>
          <p className="mb-1">
            <strong>Rating:</strong> {rating}
          </p>
          <h3 className="h5 mt-2"> ₹{price}</h3>

          {stock === 0 ? (
            <p className="badge text-bg-danger align-self-start">
              Out Of Stock
            </p>
          ) : stock <= 5 ? (
            <p className="badge text-bg-warning align-self-start">
              Only {stock} left.
            </p>
          ) : (
            <p className="badge text-bg-success align-self-start">
              In Stock: {stock}
            </p>
          )}
          <div className="mt-auto d-flex flex-wrap gap-2">
            <button
              type="button"
              className="btn btn-primary"
              disabled={stock === 0}
            >
              {stock === 0 ? "Unavailable" : "Add To Cart"}
            </button>

            {isSeller && (
              <button
                type="button"
                className="btn btn-warning"
                onClick={() => setIsEditing(true)}
              >
                Update Product
              </button>
            )}

            {isAdmin && rating <= 1 && (
              <button
                type="button"
                className="btn btn-danger"
                onClick={() => onRemoveLowRatedProduct(product.id)}
              >
                Remove Low Rated Product
              </button>
            )}
          </div>
        </div>
      </article>
      {/* <div className="product-card">
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
      </div> */}
    </>
  );
}

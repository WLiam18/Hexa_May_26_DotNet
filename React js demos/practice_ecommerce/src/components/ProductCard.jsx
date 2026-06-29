function ProductCard({ product, user, onDeleteProduct, isAdmin }) {
  const { name, price, category, stock, rating, seller } = product;

  return (
    <div>
      <h4>{name}</h4>
      <p>Price: ${price}</p>
      <p>Category: {category}</p>
      <p>Stock: {stock}</p>
      <p>Rating: {rating || "N/A"}</p>
      <p>Seller: {seller}</p>
      {isAdmin && rating <= 2 && (
        <button onClick={() => onDeleteProduct(product.id)}>
          Remove
        </button>
      )}
    </div>
  );
}

export default ProductCard;
function ProductCard({ product, user, onDeleteProduct, isAdmin }) {
  const { name, price, category, stock, rating, seller } = product;

  return (
    <div className="card h-100 shadow-sm">
      <div className="card-body">
        <h5 className="card-title">{name}</h5>
        <p className="card-text mb-1">
          <strong>Price:</strong> {price}
        </p>
        <p className="card-text mb-1">
          <strong>Category:</strong> {category}
        </p>
        <p className="card-text mb-1">
          <strong>Stock:</strong> {stock}
        </p>
        <p className="card-text mb-1">
          <strong>Rating:</strong> {rating || "N/A"}
        </p>
        <p className="card-text mb-2">
          <strong>Seller:</strong> {seller}
        </p>
        
        {isAdmin && rating <= 2 && (
          <button 
            className="btn btn-danger btn-sm mt-2"
            onClick={() => onDeleteProduct(product.id)}
          >
            Remove
          </button>
        )}
      </div>
    </div>
  );
}

export default ProductCard;
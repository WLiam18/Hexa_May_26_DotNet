function BookCard({ book }) {
  const { title, author, genre, year, rating, pages } = book;

  return (
    <div className="book-card">
      <h3>{title}</h3>
      <p className="author">{author}</p>
      <div className="book-meta">
        <span>{genre}</span>
        <span>📅 {year}</span>
        <span>⭐ {rating}</span>
        <span>📄 {pages}</span>
      </div>
    </div>
  );
}

export default BookCard;
import BookCard from "./BookCard";

function BookList({ books }) {
  if (books.length === 0) {
    return <p className="empty">No books found.</p>;
  }

  return (
    <div className="book-grid">
      {books.map((book) => (
        <BookCard key={book.id} book={book} />
      ))}
    </div>
  );
}

export default BookList;
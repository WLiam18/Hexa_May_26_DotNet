import { BookCard } from "./BookCard";

export function BookList({ books }) {
  if (books.length === 0) {
    return <p className="empty">No books found. Try a different search or filter.</p>;
  }

  return (
    <div className="book-grid">
      {books.map((book) => (
        <BookCard key={book.id} book={book} />
      ))}
    </div>
  );
}
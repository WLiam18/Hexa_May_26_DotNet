import { useState } from "react";
import { books } from "./data/books";
import { BookList } from "./components/BookList";
import { SearchBox } from "./components/SearchBox";
import { GenreFilter } from "./components/GenreFilter";
import "./App.css";

function App() {
  const [search, setSearch] = useState("");
  const [genre, setGenre] = useState("All");
  const [sortBy, setSortBy] = useState("");

  const filteredBooks = books.filter((book) => {
    const matchesSearch =
      book.title.toLowerCase().includes(search.toLowerCase()) ||
      book.author.toLowerCase().includes(search.toLowerCase());

    const matchesGenre = genre === "All" || book.genre === genre;

    return matchesSearch && matchesGenre;
  });

  const sortedBooks = [...filteredBooks].sort((a, b) => {
    if (sortBy === "yearNewest") return b.year - a.year;
    if (sortBy === "yearOldest") return a.year - b.year;
    if (sortBy === "ratingHigh") return b.rating - a.rating;
    return 0;
  });

  return (
    <div className="app">
      <h1>📚 My Book Collection</h1>
      <p className="subtitle">{books.length} books in your library</p>

      <div className="controls">
        <SearchBox search={search} onSearchChange={setSearch} />
        <GenreFilter genre={genre} onGenreChange={setGenre} />

        <select
          value={sortBy}
          onChange={(e) => setSortBy(e.target.value)}
          className="filter-select"
        >
          <option value="">Sort By</option>
          <option value="yearNewest">Newest First</option>
          <option value="yearOldest">Oldest First</option>
          <option value="ratingHigh">Highest Rated</option>
        </select>
      </div>

      <p className="result-count">
        Showing {sortedBooks.length} of {filteredBooks.length} books
      </p>

      <BookList books={sortedBooks} />
    </div>
  );
}

export default App;
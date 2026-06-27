export function SearchBox({ search, onSearchChange }) {
  return (
    <input
      type="text"
      placeholder="Search by title or author..."
      value={search}
      onChange={(e) => onSearchChange(e.target.value)}
      className="search-box"
    />
  );
}
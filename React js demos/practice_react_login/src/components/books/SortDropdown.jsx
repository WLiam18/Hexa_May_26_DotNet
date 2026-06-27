function SortDropdown({ sortBy, onSortChange }) {
  return (
    <select
      value={sortBy}
      onChange={(e) => onSortChange(e.target.value)}
      className="filter-select"
    >
      <option value="">Sort By</option>
      <option value="yearNewest">Newest First</option>
      <option value="yearOldest">Oldest First</option>
      <option value="ratingHigh">Highest Rated</option>
    </select>
  );
}

export default SortDropdown;
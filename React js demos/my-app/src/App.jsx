import React, { useState } from 'react';

// 1. Variables
var oldWay = "Avoid this";
let mutable = "Can change";
const fixed = "Cannot change";

// 2. Arrow functions
const add = (a, b) => a + b;
const greet = (name) => `Hello ${name}`;
const getBook = (title, author) => ({ title, author });

// 3. Destructuring
const user = {
    name: "Alice",
    age: 25,
    city: "Mumbai"
};

const { name, age, city } = user;

const colors = ["Red", "Green", "Blue", "Yellow"];
const [first, second, ...rest] = colors;

// 4. Spread operator
const frontend = ["HTML", "CSS", "JS"];
const backend = ["C#", "SQL"];

const all = [...frontend, ...backend];
const person = { name: "John", age: 30 };
const updatedPerson = { ...person, city: "Delhi" };

// 5. Rest operator
function showCourses(...courses) {
    console.log(courses);
}

const tasks = ["Task1", "Task2", "Task3", "Task4"];
const [task1, task2, ...remaining] = tasks;

// 6. Ternary operator
const isLoggedIn = true;
const status = isLoggedIn ? "Logged In" : "Logged Out";

function App() {
    const [show, setShow] = useState(false);

    const books = [
        { id: 1, title: "Csharp FUndamentals", author: "William" },
        { id: 2, title: "Adventures of Tintin", author: "Orwell" },
        { id: 3, title: "Diary of a  wimpy kid", author: "Jeff" }
    ];

    const bookTitles = books.map(book => book.title);
    const totalBooks = books.length;

    return (
        <div style={{ padding: "20px", fontFamily: "Arial" }}>
            <h1>Books</h1>

            <p>Total: {totalBooks}</p>

            <ul>
                {books.map(({ id, title, author }) => (
                    <li key={id}>
                        {title} - {author}
                    </li>
                ))}
            </ul>

            <button onClick={() => setShow(!show)}>
                {show ? "Hide" : "Show"} Details
            </button>

            {show && (
                <div style={{ marginTop: "10px", border: "1px solid #ccc", padding: "10px" }}>
                    <p>First book: {books[0].title}</p>
                    <p>Last book: {books[books.length - 1].title}</p>
                    <p>All titles: {bookTitles.join(", ")}</p>
                </div>
            )}
        </div>
    );
}

export default App;
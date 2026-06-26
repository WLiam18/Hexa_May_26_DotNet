var oldway = 1; //function-scoped,hoisted,avoid in modern mode
let mutable = 2; //block-scoped, can be reassigned
const fixed = 3; // block scoped cannot be reassigned

console.log(oldway);

console.log(mutable);

console.log(fixed);

//arrow fucntion
const add = (x, y) => x + y;
console.log(add(100, 400));

const greet = (name) => `Hi ${name} welcome.`;
console.log(greet("Tina"));

const make = () => ({ ok: true });
console.log(make());

//destructuring

const book = { title: "Dune", author: "Herbert" };
const { title, author } = book;
console.log(book);
console.log(title);
console.log(author);

//spread operaton

const frondendCourses = ["HTML", "CSS", "Javascript"];
const backednCourses = ["C#", "ASP.NET core", "SQL Server"];
const copiedCourses = [...frondendCourses];
console.log(copiedCourses);

const allCourses = [...frondendCourses, ...backednCourses];
console.log(allCourses);

const student = {
  name: "Harini",
  course: "HTML",
  mode: "Online",
};

console.log(student);
const updatedStudent = {
  ...student,
  course: "Javascript",
};

console.log(updatedStudent);

//Rest Operator

const courses = ["HTML", "CSS", "Javascript", "React"];
// const firstCourse = courses[0];
// const remainingCourses = courses.slice(2);

// console.log(firstCourse);
// console.log(remainingCourses);

//modern way using Rest operator
const [firstCourse, ...remainingCourses] = courses;
console.log(firstCourse);
console.log(remainingCourses);

function showCourses(...courses) {
  console.log(courses);
}

showCourses("C#", "ASP.NET Core", "Azure");

// //ternary operator
// const label=isRead?"Read":"Unread";
// const maybe=isLoggedIn && <Dashboard>;

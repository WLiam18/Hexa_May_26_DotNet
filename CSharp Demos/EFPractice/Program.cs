using EFPractice.Data;
using EFPractice.Models;

namespace EFPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
                // Add a student
                var student = new Student
                {
                    Name = "John Doe",
                    Email = "john@test.com",
                    Age = 20
                };

                db.Students.Add(student);
                db.SaveChanges();

                Console.WriteLine("Student added!");

                // Show all students
                var students = db.Students.ToList();
                foreach (var s in students)
                {
                    Console.WriteLine($"ID: {s.StudentId}, Name: {s.Name}, Email: {s.Email}, Age: {s.Age}");
                }
            }

            Console.ReadLine();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using SchoolDBFirst.Data;
using SchoolDBFirst.Models;

namespace SchoolDBFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchoolDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=SchoolDB;User Id=sa;Password=StrongPass@123!;TrustServerCertificate=true;");

            using (var db = new SchoolDbContext(optionsBuilder.Options))
            {
                Console.WriteLine("=== STUDENTS AND COURSES ===\n");

                // Display Students
                Console.WriteLine("All Students:");
                var students = db.Students.ToList();
                foreach (var s in students)
                {
                    Console.WriteLine($"  ID: {s.StudentId}, Name: {s.Name}, Email: {s.Email}, Age: {s.Age}");
                }

                // Display Courses
                Console.WriteLine("\nAll Courses:");
                var courses = db.Courses.ToList();
                foreach (var c in courses)
                {
                    Console.WriteLine($"  ID: {c.CourseId}, Name: {c.CourseName}, Credits: {c.Credits}");
                }

                // Add William
                Console.WriteLine("\n--- Adding William ---");
                var william = new Student
                {
                    Name = "William",
                    Email = "william@test.com",
                    Age = 22
                };
                db.Students.Add(william);
                db.SaveChanges();
                Console.WriteLine($"Added: {william.Name} (ID: {william.StudentId})");

                // Add Joel
                Console.WriteLine("--- Adding Joel ---");
                var joel = new Student
                {
                    Name = "Joel",
                    Email = "joel@test.com",
                    Age = 22
                };
                db.Students.Add(joel);
                db.SaveChanges();
                Console.WriteLine($"Added: {joel.Name} (ID: {joel.StudentId})");

                // Final list
                Console.WriteLine("\n=== Final Student List ===");
                var finalStudents = db.Students.ToList();
                foreach (var s in finalStudents)
                {
                    Console.WriteLine($"  {s.Name} - Age {s.Age} ({s.Email})");
                }
            }

            Console.ReadLine();
        }
    }
}
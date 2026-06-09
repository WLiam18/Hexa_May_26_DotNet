CREATE DATABASE SchoolDB;
GO

USE SchoolDB;
GO

CREATE TABLE Students (
    StudentId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Age INT NOT NULL
);
GO

CREATE TABLE Courses (
    CourseId INT PRIMARY KEY IDENTITY(1,1),
    CourseName NVARCHAR(100) NOT NULL,
    Credits INT NOT NULL
);
GO

INSERT INTO Students (Name, Email, Age) VALUES 
('William', 'william@test.com', 22),
('joel', 'joel@test.com', 22);
GO

INSERT INTO Courses (CourseName, Credits) VALUES 
('Mathematics', 4),
('Computer Science', 3);
GO


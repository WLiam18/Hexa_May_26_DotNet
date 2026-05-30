use Hexa_May_26_DotNet


Select * from Employees;
select * from Department

-- single row sub-query

SELECT * FROM Employees WHERE DepartmentId=
(
SELECT DepartmentId from Department Where DepartmentName='IT'
)

-- Scalar subquery
SELECT * FROM Employees
WHERE Salary >
(
select Avg(salary) from Employees
);

-- MutliRow Sub Query using in
SELECT * from Employees Where DepartmentId in
(
select DepartmentId from Department 
where Location in ('Chennai','Bangalore')
);

select * from Department;

-- MutliRow Sub Query using NOT IN

select * from Employees
Where DepartmentId NOT IN
(
Select DepartmentId from Department Where Location='Chennai'
)

-- Subquery With ANY operator
select * from Employees Where Salary>ANY(
select salary from Employees where DepartmentId=
(
select DepartmentId from Department Where DepartmentName='IT')
);

-- Subquery With ALL operator
select * from Employees Where Salary>=ALL(
select salary from Employees where DepartmentId=
(
select DepartmentId from Department Where DepartmentName='IT')
);


-- Subquery with EXISTS
SELECT * FROM Department d
where Exists
(
select * from Employees e
WHERE e.DepartmentId=d.DepartmentId
)


SELECT * FROM Department d
where NOT Exists
(
select * from Employees e
WHERE e.DepartmentId=d.DepartmentId
)
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

SELECT * FROM Employees;
Select * from Department;

--user defined function (UDF)

/*

Types of user defined function
1.Scalar Function -> Single value
2.Inline table-valued function -> table result
3. Multistatement table valued function ->  it allows multiple SQL statements

*/

-- Example for Scalar function


CREATE FUNCTION dbo.GetAnnualSalary
(
@Salary DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
AS
BEGIN
	DECLARE @AnnualSalary DECIMAL(10,2);

	SET @AnnualSalary=@Salary*12;

	RETURN @AnnualSalary;
END;


SELECT dbo.GetAnnualSalary(35000) as AnnualSalary;

SELECT EmployeeId,Name,Salary,dbo.GetAnnualSalary(salary)  as AnnualSalary
FROM Employees;


-- Handling NULL Salary in Function
CREATE FUNCTION dbo.GetAnnualSalaryWithNullCheck
(
@Salary DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
AS
BEGIN
	DECLARE @AnnualSalary DECIMAL(10,2);

	SET @AnnualSalary=ISNULL(@Salary,0)*12;

	RETURN @AnnualSalary;
END;


SELECT EmployeeId,Name,Salary,dbo.GetAnnualSalaryWithNullCheck(salary)  as AnnualSalary
FROM Employees

-- Scalar function to calculate the Bonus

CREATE FUNCTION dbo.GetEmployeeBonus
(
@Salary DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
AS
BEGIN
	DECLARE @Bonus DECIMAL(10,2);

	IF @Salary IS NULL
		SET @Bonus=0;
	ELSE IF @Salary>38000
		SET @Bonus=@Salary*0.10;
	ELSE IF @Salary>=35000
		SET @Bonus=@Salary*0.08;
	ELSE
		SET @Bonus=@Salary*0.05;

	RETURN @Bonus;
END;

SELECT EmployeeId,Name,Salary,dbo.GetEmployeeBonus(salary)  as AnnualSalary
FROM Employees


-- inline table  valued function
CREATE FUNCTION dbo.GetEmployeeByDepartment
(@DepartmentId INT)
RETURNS TABLE
AS
RETURN
	(
	SELECT EmployeeId,Name,Gender,City,Salary,Email,MobileNo,DepartmentId
	FROM Employees
	WHERE DepartmentId=@DepartmentId
	);


SELECT * FROM dbo.GetEmployeeByDepartment(2);


-- Write a user defined function to Get Employees with Department Details (use joins)
-- Write a user defined function to Get Employees by salary Range (use 2 paramters minsalary and max salary)


--Multi-Statement Table valued function

CREATE FUNCTION dbo.GetEmployeeSalaryReport
(@DepartmentId INT)
RETURNS @SalaryReport TABLE
(
EmployeeId INT,
Name VARCHAR(50),
DepartmentName VARCHAR(20),
Salary DECIMAL(10,2),
AnnualSalary DECIMAL(10,2),
BonusAmount DECIMAL(10,2),
SalaryGrade VARCHAR(30)
)
AS
BEGIN
	 INSERT INTO @SalaryReport
	 SELECT
	 e.EmployeeId,
	 e.Name,
	 d.DepartmentName,
	 e.Salary,
	 ISNULL(e.Salary,0)*12 AS AnnualSalary,
	 CASE
		WHEN e.Salary IS NULL THEN 0
		WHEN e.Salary>=38000 THEN e.Salary*0.10
		WHEN e.Salary>=35000 THEN e.Salary*0.08
		ELSE e.Salary*0.05
	END AS BonusAmount,
	CASE
		WHEN e.Salary IS NULL THEN 'Not Available'
		WHEN e.Salary>=38000 THEN 'High Salary'
		WHEN e.Salary>=35000 THEN 'Medium Salary'
		ELSE 'Low Salary'
	END As SalaryGrade
 FROM Employees e
 INNER JOIN Department d
 ON d.DepartmentId=e.DepartmentId
 WHERE e.DepartmentId=@DepartmentId

 RETURN;
END;


SELECT * FROM dbo.GetEmployeeSalaryReport(2);

Select * from  Employees



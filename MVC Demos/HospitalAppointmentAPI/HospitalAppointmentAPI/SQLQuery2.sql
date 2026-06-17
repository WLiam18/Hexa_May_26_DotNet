IF OBJECT_ID('dbo.Roles') IS NULL
BEGIN
  CREATE TABLE dbo.Roles (
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL
  );
END

IF OBJECT_ID('dbo.Users') IS NULL
BEGIN
  CREATE TABLE dbo.Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(500) NOT NULL,
    FullName NVARCHAR(200) NOT NULL
  );
END

IF OBJECT_ID('dbo.UserRoles') IS NULL
BEGIN
  CREATE TABLE dbo.UserRoles (
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    CONSTRAINT PK_UserRoles PRIMARY KEY (UserId, RoleId),
    CONSTRAINT FK_UserRoles_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(UserId),
    CONSTRAINT FK_UserRoles_Roles FOREIGN KEY (RoleId) REFERENCES dbo.Roles(RoleId)
  );
END


INSERT INTO dbo.Roles ([Name]) VALUES ('Admin'), ('User'), ('Doctor');

-- Demo admin user (Password stored as plaintext in PasswordHash column for this demo)
INSERT INTO dbo.Users (Username, PasswordHash, FullName) VALUES ('admin','adminpass','Administrator');

-- Assign Admin role to admin user (adjust UserId/RoleId if identity values differ)
INSERT INTO dbo.UserRoles (UserId, RoleId)
VALUES ((SELECT UserId FROM dbo.Users WHERE Username='admin'), (SELECT RoleId FROM dbo.Roles WHERE [Name]='Admin'));

-- ============================================
-- INSERT DOCTOR USERS
-- ============================================
INSERT INTO dbo.Users (Username, PasswordHash, FullName)
VALUES 
('dr_aravind', 'aravinpass123', 'Dr. Aravind Kumar'),
('dr_meera', 'meerapass123', 'Dr. Meera Nair'),
('dr_suresh', 'sureshpass123', 'Dr. Suresh Babu');

-- ============================================
-- INSERT PATIENT USERS
-- ============================================
INSERT INTO dbo.Users (Username, PasswordHash, FullName)
VALUES 
('ravi_chandran', 'ravipass123', 'Ravi Chandran'),
('priya_raman', 'priyapass123', 'Priya Raman'),
('karthik_raj', 'karthikpass123', 'Karthik Raj');

-- ============================================
-- MAP DOCTOR USERS TO DOCTOR ROLE
-- ============================================
INSERT INTO dbo.UserRoles (UserId, RoleId)
SELECT u.UserId, r.RoleId FROM dbo.Users u, dbo.Roles r 
WHERE u.Username IN ('dr_aravind', 'dr_meera', 'dr_suresh') AND r.[Name] = 'Doctor';

-- ============================================
-- MAP PATIENT USERS TO USER ROLE
-- ============================================
INSERT INTO dbo.UserRoles (UserId, RoleId)
SELECT u.UserId, r.RoleId FROM dbo.Users u, dbo.Roles r 
WHERE u.Username IN ('ravi_chandran', 'priya_raman', 'karthik_raj') AND r.[Name] = 'User';

select * from users;
select * from roles;
select * from UserRoles;

-- ============================================
-- VERIFY USER ROLES
-- ============================================
SELECT u.UserId, u.Username, u.FullName, r.[Name] AS RoleName
FROM dbo.UserRoles ur
JOIN dbo.Users u ON ur.UserId = u.UserId
JOIN dbo.Roles r ON ur.RoleId = r.RoleId
ORDER BY u.UserId;


select * from Appointments;
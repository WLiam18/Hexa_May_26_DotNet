

USE HospitalAppointmentDb;
GO

CREATE TABLE Doctors
(
    DoctorId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Specialization NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    PhoneNumber NVARCHAR(20) NOT NULL,
    ConsultationFee DECIMAL(18,2) NOT NULL,
    IsAvailable BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Patients
(
    PatientId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    PhoneNumber NVARCHAR(20) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender NVARCHAR(20) NOT NULL,
    Address NVARCHAR(250) NULL
);
GO

CREATE TABLE Appointments
(
    AppointmentId INT IDENTITY(1,1) PRIMARY KEY,
    DoctorId INT NOT NULL,
    PatientId INT NOT NULL,
    AppointmentDate DATE NOT NULL,
    AppointmentTime TIME NOT NULL,
    AppointmentStatus NVARCHAR(30) NOT NULL DEFAULT 'Scheduled',
    Reason NVARCHAR(250) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT FK_Appointments_Doctors
        FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId),

    CONSTRAINT FK_Appointments_Patients
        FOREIGN KEY (PatientId) REFERENCES Patients(PatientId),

    CONSTRAINT CK_Appointments_Status
        CHECK (AppointmentStatus IN ('Scheduled', 'Completed', 'Cancelled'))
);
GO

INSERT INTO Doctors
(FullName, Specialization, Email, PhoneNumber, ConsultationFee, IsAvailable)
VALUES
('Dr. Aravind Kumar', 'Cardiology', 'aravind.kumar@hospital.com', '9876543210', 800.00, 1),
('Dr. Meera Nair', 'Dermatology', 'meera.nair@hospital.com', '9876543211', 600.00, 1),
('Dr. Suresh Babu', 'Orthopedics', 'suresh.babu@hospital.com', '9876543212', 700.00, 1);
GO

INSERT INTO Patients
(FullName, Email, PhoneNumber, DateOfBirth, Gender, Address)
VALUES
('Ravi Chandran', 'ravi.chandran@gmail.com', '9000000001', '1990-05-12', 'Male', 'Chennai'),
('Priya Raman', 'priya.raman@gmail.com', '9000000002', '1995-09-20', 'Female', 'Coimbatore'),
('Karthik Raj', 'karthik.raj@gmail.com', '9000000003', '1988-11-05', 'Male', 'Madurai');
GO

INSERT INTO Appointments
(DoctorId, PatientId, AppointmentDate, AppointmentTime, AppointmentStatus, Reason)
VALUES
(1, 1, '2026-07-10', '10:30:00', 'Scheduled', 'Chest pain consultation'),
(2, 2, '2026-07-11', '11:00:00', 'Scheduled', 'Skin allergy'),
(3, 3, '2026-07-12', '15:30:00', 'Scheduled', 'Knee pain');
GO

select * from Doctors;
select * from Patients;
select * from Appointments;
select * from Users;
select * from Roles;
select * from UserRoles;
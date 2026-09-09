CREATE DATABASE HospitalAuthSPDb;
GO

USE HospitalAuthSPDb;
GO


CREATE TABLE Users
(
    Id INT IDENTITY PRIMARY KEY,
    UserName NVARCHAR(50),
    FullName NVARCHAR(100),
    Email NVARCHAR(100),
    Phone NVARCHAR(20),
    Password NVARCHAR(100),
    Role NVARCHAR(20)
);
GO


CREATE PROCEDURE sp_LoginUser
    @Email NVARCHAR(100),
    @Password NVARCHAR(100)
AS
BEGIN
    SELECT Id, UserName, Email, Role
    FROM Users
    WHERE Email = @Email
      AND Password = @Password;
END;
GO


CREATE PROCEDURE sp_RegisterDoctor
    @UserName NVARCHAR(50),
    @FullName NVARCHAR(100),
    @Specialization NVARCHAR(100),
    @Department NVARCHAR(100),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Password NVARCHAR(100)
AS
BEGIN
    INSERT INTO Users
    (
        UserName,
        FullName,
        Email,
        Phone,
        Password,
        Role
    )
    VALUES
    (
        @UserName,
        @FullName,
        @Email,
        @Phone,
        @Password,
        'Doctor'
    );
END;
GO


CREATE PROCEDURE sp_RegisterNurse
    @UserName NVARCHAR(50),
    @FullName NVARCHAR(100),
    @Email NVARCHAR(100),
    @Department NVARCHAR(100),
    @Shift NVARCHAR(50),
    @Address NVARCHAR(200),
    @Password NVARCHAR(100)
AS
BEGIN
    INSERT INTO Users
    (
        UserName,
        FullName,
        Email,
        Password,
        Role
    )
    VALUES
    (
        @UserName,
        @FullName,
        @Email,
        @Password,
        'Nurse'
    );
END;
GO


CREATE PROCEDURE sp_RegisterPatient
    @FullName NVARCHAR(100),
    @Email NVARCHAR(100),
    @Gender NVARCHAR(10),
    @Age INT,
    @Phone NVARCHAR(20),
    @Address NVARCHAR(200),
    @Password NVARCHAR(100)
AS
BEGIN
    INSERT INTO Users
    (
        FullName,
        Email,
        Phone,
        Password,
        Role
    )
    VALUES
    (
        @FullName,
        @Email,
        @Phone,
        @Password,
        'Patient'
    );
END;
GO

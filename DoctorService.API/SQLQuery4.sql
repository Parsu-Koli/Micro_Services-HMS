/*CREATE DATABASE HospitalMSDb;
GO


USE HospitalMSDb;
GO

CREATE TABLE Doctors
(
    Id INT IDENTITY PRIMARY KEY,
    UserName NVARCHAR(50),
    FullName NVARCHAR(100),
    Specialization NVARCHAR(100),
    Department NVARCHAR(100),
    Email NVARCHAR(100),
    Phone NVARCHAR(20),
    Password NVARCHAR(200)
);

CREATE PROCEDURE sp_Doctor_CRUD
(
    @Flag NVARCHAR(20),
    @Id INT = NULL,
    @UserName NVARCHAR(50) = NULL,
    @FullName NVARCHAR(100) = NULL,
    @Specialization NVARCHAR(100) = NULL,
    @Department NVARCHAR(100) = NULL,
    @Email NVARCHAR(100) = NULL,
    @Phone NVARCHAR(20) = NULL,
    @Password NVARCHAR(200) = NULL
)
AS
BEGIN
    IF @Flag = 'CREATE'
        INSERT INTO Doctors
        VALUES (@UserName,@FullName,@Specialization,@Department,@Email,@Phone,@Password)

    ELSE IF @Flag = 'GET_ALL'
        SELECT Id,UserName,FullName,Specialization,Department,Email,Phone FROM Doctors

    ELSE IF @Flag = 'GET_BY_ID'
        SELECT Id,UserName,FullName,Specialization,Department,Email,Phone FROM Doctors WHERE Id=@Id

    ELSE IF @Flag = 'UPDATE'
        UPDATE Doctors
        SET UserName=@UserName,
            FullName=@FullName,
            Specialization=@Specialization,
            Department=@Department,
            Email=@Email,
            Phone=@Phone
        WHERE Id=@Id

    ELSE IF @Flag = 'DELETE'
        DELETE FROM Doctors WHERE Id=@Id
END


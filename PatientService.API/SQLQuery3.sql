/*
CREATE DATABASE HospitalMSDb;
GO
USE HospitalMSDb;
GO


CREATE TABLE Patients
(
    Id INT IDENTITY PRIMARY KEY,
    FullName NVARCHAR(100),
    Email NVARCHAR(100),
    Gender NVARCHAR(10),
    Age INT,
    Phone NVARCHAR(20),
    Address NVARCHAR(200)
);


CREATE PROCEDURE sp_Patient_CRUD
(
    @Flag NVARCHAR(20),
    @Id INT = NULL,
    @FullName NVARCHAR(100) = NULL,
    @Email NVARCHAR(100) = NULL,
    @Gender NVARCHAR(10) = NULL,
    @Age INT = NULL,
    @Phone NVARCHAR(20) = NULL,
    @Address NVARCHAR(200) = NULL
)
AS
BEGIN
    IF @Flag = 'CREATE'
        INSERT INTO Patients VALUES (@FullName,@Email,@Gender,@Age,@Phone,@Address)

    ELSE IF @Flag = 'GET_ALL'
        SELECT * FROM Patients

    ELSE IF @Flag = 'GET_BY_ID'
        SELECT * FROM Patients WHERE Id=@Id

    ELSE IF @Flag = 'UPDATE'
        UPDATE Patients
        SET FullName=@FullName, Age=@Age, Phone=@Phone, Address=@Address
        WHERE Id=@Id

    ELSE IF @Flag = 'DELETE'
        DELETE FROM Patients WHERE Id=@Id
END



*/


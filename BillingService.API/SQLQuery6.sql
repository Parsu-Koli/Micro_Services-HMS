/*CREATE DATABASE HospitalMSDb;
GO
USE HospitalMSDb;
GO


CREATE TABLE Bills
(
    Id INT IDENTITY PRIMARY KEY,
    AppointmentId INT,
    PatientId INT,
    DoctorId INT,
    Amount DECIMAL(10,2),
    PaymentStatus NVARCHAR(20) DEFAULT 'Pending',
    CreatedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (AppointmentId) REFERENCES Appointments(Id),
    FOREIGN KEY (PatientId) REFERENCES Patients(Id),
    FOREIGN KEY (DoctorId) REFERENCES Doctors(Id)
);


CREATE PROCEDURE sp_Billing_CRUD
(
    @Flag NVARCHAR(20),
    @Id INT = NULL,
    @AppointmentId INT = NULL,
    @PatientId INT = NULL,
    @DoctorId INT = NULL,
    @Amount DECIMAL(10,2) = NULL,
    @PaymentStatus NVARCHAR(20) = NULL
)
AS
BEGIN
    IF @Flag = 'CREATE'
        INSERT INTO Bills VALUES (@AppointmentId,@PatientId,@DoctorId,@Amount,'Pending',GETDATE())

    ELSE IF @Flag = 'GET_ALL'
        SELECT * FROM Bills

    ELSE IF @Flag = 'GET_BY_PATIENT'
        SELECT * FROM Bills WHERE PatientId=@PatientId

    ELSE IF @Flag = 'UPDATE_PAYMENT'
        UPDATE Bills SET PaymentStatus=@PaymentStatus WHERE Id=@Id

    ELSE IF @Flag = 'DELETE'
        DELETE FROM Bills WHERE Id=@Id
END

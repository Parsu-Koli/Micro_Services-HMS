/*CREATE DATABASE HospitalMSDb;
GO
USE HospitalMSDb;
GO


CREATE TABLE Appointments
(
    Id INT IDENTITY PRIMARY KEY,
    PatientId INT,
    DoctorId INT,
    AppointmentDate DATETIME,
    Status NVARCHAR(20) DEFAULT 'Pending',
    CreatedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (PatientId) REFERENCES Patients(Id),
    FOREIGN KEY (DoctorId) REFERENCES Doctors(Id)
);

CREATE PROCEDURE sp_Appointment_CRUD
(
    @Flag NVARCHAR(20),
    @Id INT = NULL,
    @PatientId INT = NULL,
    @DoctorId INT = NULL,
    @AppointmentDate DATETIME = NULL,
    @Status NVARCHAR(20) = NULL
)
AS
BEGIN
    IF @Flag = 'CREATE'
        INSERT INTO Appointments VALUES (@PatientId,@DoctorId,@AppointmentDate,'Pending',GETDATE())

    ELSE IF @Flag = 'GET_ALL'
        SELECT 
            A.Id,
            P.FullName AS PatientName,
            D.FullName AS DoctorName,
            A.AppointmentDate,
            A.Status
        FROM Appointments A
        JOIN Patients P ON A.PatientId = P.Id
        JOIN Doctors D ON A.DoctorId = D.Id

    ELSE IF @Flag = 'UPDATE_STATUS'
        UPDATE Appointments SET Status=@Status WHERE Id=@Id

    ELSE IF @Flag = 'DELETE'
        DELETE FROM Appointments WHERE Id=@Id
END


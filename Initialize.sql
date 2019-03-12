CREATE PROCEDURE dbo.usp_CreateDepartment 
    @DName NVARCHAR (50),
    @MgrSSN NUMERIC (9)  
AS
    BEGIN
		DECLARE @DepNumber AS INTEGER;

		SET @DepNumber = (SELECT MAX(DNumber) + 1 FROM Department)
		INSERT INTO Department(DNumber, DName, MgrSSN, MgrStartDate)
		VALUES (@DepNumber, @DName, @MgrSSN, GETDATE())
	END
RETURN @DepNumber

GO
EXEC dbo.usp_CreateDepartment "a", 123456789
GO
SELECT * FROM Department WHERE DName = "a"

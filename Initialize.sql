
/*ALTER PROCEDURE dbo.usp_CreateDepartment 
    @DName NVARCHAR (50),
    @MgrSSN NUMERIC (9)  
AS
    BEGIN
		
		IF EXISTS (SELECT 1 FROM Department WHERE DName = @DName)
			THROW 50001, 'Depmartment name already exists!', 1;

		IF EXISTS (SELECT 1 FROM Department WHERE MgrSSN = @MgrSSN)
			THROW 50002, 'Department manager already exists!', 1;
		

		DECLARE @DepNumber AS INTEGER;

		SET @DepNumber = (SELECT MAX(DNumber) + 1 FROM Department)
		INSERT INTO Department(DNumber, DName, MgrSSN, MgrStartDate)
		VALUES (@DepNumber, @DName, @MgrSSN, GETDATE())
	END
RETURN @DepNumber
GO
*/

-- Assignment 1.B
ALTER PROCEDURE dbo.usp_UpdateDepartmentName 
	@DNumber INTEGER,
	@DName NVARCHAR(50)
AS
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM Department WHERE DNumber = @DNumber)
			THROW 50003, 'Department number does not exist!', 1;
		IF EXISTS (SELECT 1 FROM Department WHERE DName = @DName)
			THROW 50001, 'Depmartment name already exists!', 1;

		UPDATE Department
		SET DName = @DName
		WHERE DNumber = @DNumber
	END
RETURN;

GO
EXEC dbo.usp_UpdateDepartmentName 6, 'test' -- Should throw Msg 50001
GO
SELECT * FROM Department WHERE DNumber = 6

/*
GO
EXEC dbo.usp_CreateDepartment 'Administration', 123456789
GO
SELECT * FROM Department WHERE DName = 'Administration'
*/
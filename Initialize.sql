﻿
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

--e
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'usp_GetDepartment'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.usp_GetDepartment
GO
CREATE PROCEDURE dbo.usp_GetDepartment
    @DNo int
AS
    SELECT d.*, (SELECT Count(*) FROM Employee e WHERE e.Dno = d.DNumber) as EmployeeCount FROM Department d WHERE d.DNumber = @DNo
GO
EXECUTE dbo.usp_GetDepartment 1
GO

--f
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'usp_GetAllDepartments'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.usp_GetAllDepartments
GO
CREATE PROCEDURE dbo.usp_GetAllDepartments
AS
    SELECT d.*, (SELECT Count(*) FROM Employee e WHERE e.Dno = d.DNumber) as EmployeeCount FROM Department d
GO
EXECUTE dbo.usp_GetAllDepartments
GO



GO
ALTER PROCEDURE dbo.usp_DeleteDepartment(@DNumberDelete INT)
AS

		IF EXISTS (SELECT 1 FROM Department WHERE DNumber = @DNumberDelete)		
		BEGIN

			--Declare table with all project numbers from the departrment that will be deleted
			DECLARE @ProjectNoTbl TABLE (ProjectNo INTEGER);	
			INSERT INTO @ProjectNoTbl (ProjectNo) (SELECT PNumber FROM Project WHERE DNum = @DNumberDelete)

			--Delete everything ralated to the department
			DELETE FROM Works_on WHERE Pno in (SELECT ProjectNo FROM @ProjectNoTbl);
			DELETE FROM Project WHERE DNum = @DNumberDelete;
			DELETE FROM Dept_Locations WHERE DNUmber = @DNumberDelete;
			DELETE FROM Department WHERE DNumber = @DNumberDelete;
			
			UPDATE Employee SET Dno = NULL WHERE Dno = @DNumberDelete;
			
			END

RETURN;

GO
EXEC dbo.usp_DeleteDepartment 4
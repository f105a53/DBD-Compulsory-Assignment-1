CREATE PROCEDURE dbo.usp_CreateDepartment 
    @DName NVARCHAR (50),
    @MgrSSN NUMERIC (9)  
AS
    BEGIN
		DECLARE @DepNumber AS INTEGER;
		/*
		DECLARE @HighestDepNumber TABLE (DName NVARCHAR(50) NULL,
										 DNumber INTEGER NOT NULL,
										 MgrSSN NUMERIC(9, 0) NULL,
										 MgrStartDate DATETIME NULL)
		
		*/

		SELECT @DepNumber = [DNumber]
		FROM Department
		



		SELECT MAX(DNumber) INTO #DepNumber FROM Department
		--SELECT MAX(DNumber) +1 INTO @DepNumber FROM Department

		INSERT INTO Department (DName, DNumber, MgrSSN, MgrStartDate)
		SELECT 


		INSERT INTO Department(DNumber, DName, MgrSSN)
		VALUES (@DepNumber, @DName, @MgrSSN)
	END
RETURN @DepNumber

GO
EXEC dbo.usp_CreateDepartment "test", 123456789
GO
SELECT * FROM Department WHERE DName = "test"

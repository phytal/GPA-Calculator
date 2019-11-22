
# **GPA Calculator**
### *This is a WPF made by William Zhang.*
---
## Summary
### In a nutshell this is an WPF that takes grades from the Frisco ISD home access center and processes it which then returns your weighted and unweighted GPA. Extra features include the ability to exclude classes from the GPA calculation and uses an SQL server to store user data.
___
## SQL Code
### This is for those who wish to run this application on their computer.
This is the SQL code for DeleteUser
```
USE [GPACalculator]
GO
/****** Object:  StoredProcedure [dbo].[GPACalculator_DeleteUser]    Script Date: 11/21/2019 10:30:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GPACalculator_DeleteUser]
	-- Add the parameters for the stored procedure here
	@Username nvarchar(50),
	@Password nvarchar(50), 
	@Name nvarchar(50) 
AS
BEGIN
	SET NOCOUNT ON;

delete from [dbo].[Users]
where [Username] = @Username and [Password] = @Password and [Name] = @Name
END

```
---
This is the SQL code for EditUser
```
USE [GPACalculator]
GO
/****** Object:  StoredProcedure [dbo].[GPACalculator_EditUser]    Script Date: 11/21/2019 10:33:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GPACalculator_EditUser]
	-- Add the parameters for the stored procedure here
	@Username nvarchar(50),
	@Password nvarchar(50), 
	@Name nvarchar(50) 
AS
BEGIN
	SET NOCOUNT ON;

update Users set [Username] = @Username, [Password] = @Password, [Name] = @Name

END

```
---
This is the SQL code for InsertUser
```
USE [GPACalculator]
GO
/****** Object:  StoredProcedure [dbo].[GPACalculator_InsertUser]    Script Date: 11/21/2019 10:33:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GPACalculator_InsertUser]
	-- Add the parameters for the stored procedure here
	@Username nvarchar(50),
	@Password nvarchar(50), 
	@Name nvarchar(50) 
AS
BEGIN
	SET NOCOUNT ON;

INSERT INTO [dbo].[Users]
VALUES (@Username, @Password, @Name)
END

```
---
This is the SQL code for UserByName (Searching users)
```
USE [GPACalculator]
GO
/****** Object:  StoredProcedure [dbo].[GPACalculator_UserByName]    Script Date: 11/21/2019 10:33:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GPACalculator_UserByName]
	-- Add the parameters for the stored procedure here
	@Name nvarchar(50) 
AS
BEGIN
	SET NOCOUNT ON;

    select *
	from dbo.Users
	where Name = @Name
END


```

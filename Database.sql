/****** Developed by: Fernando Calmet Ramirez <github.com/fernandocalmet> ******/
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'CustomersDB')
BEGIN
	CREATE DATABASE [CustomersDB]
END
GO
USE [CustomersDB]
GO

/****** Object:  Table [dbo].[customer] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[last_name] [varchar](50) NOT NULL,
	[address] [nvarchar](100) NOT NULL,
	[city] [nvarchar](100) NULL,
	[email] [nvarchar](100) NULL,
	[phone] [nvarchar](25) NULL,
	[job] [nvarchar](70) NULL,
 CONSTRAINT [PK_module_category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object: Store Procedure [dbo].[search_customer_sp] ******/
CREATE PROC [dbo].[search_customer_sp]
@name nvarchar(30)
AS
	SELECT * FROM customer WHERE first_name like @name+'%' or last_name like @name+'%' 
GO

/****** Object: Store Procedure [dbo].[create_customer_sp] ******/
CREATE PROC [dbo].[create_customer_sp]
	@first_name [varchar](50),
	@last_name [varchar](50),
	@address [nvarchar](100),
	@city [nvarchar](100),
	@email [nvarchar](100),
	@phone [nvarchar](25),
	@job [nvarchar](70)
AS
	INSERT INTO customer
	VALUES(
		@first_name,
		@last_name,
		@address,
		@city,
		@email,
		@phone,
		@job
	)
GO

/****** Object: Store Procedure [dbo].[update_customer_sp] ******/
CREATE PROC [dbo].[update_customer_sp]
	@first_name [varchar](50),
	@last_name [varchar](50),
	@address [nvarchar](100),
	@city [nvarchar](100),
	@email [nvarchar](100),
	@phone [nvarchar](25),
	@job [nvarchar](70),
	@id [int]
AS
	UPDATE customer
	SET
		first_name = @first_name,
		last_name = @last_name,
		address = @address,
		city = @city,
		email = @email,
		phone = @phone,
		job = @job
	WHERE id = @id	
GO

/****** Object: Store Procedure [dbo].[delete_customer_sp] ******/
CREATE PROC [dbo].[delete_customer_sp]
@id [int] 
AS
	DELETE FROM customer WHERE id = @id;
GO

/****** Object: Store Procedure [dbo].[select_customer_sp] ******/
CREATE PROC [dbo].[select_customer_sp]
@id [int]
AS
	SELECT * FROM customer WHERE id = @id
GO

/****** Object: Store Procedure [dbo].[select_all_customers_sp] ******/
CREATE PROC [dbo].[select_all_customers_sp]
AS
	SELECT * FROM customer
GO

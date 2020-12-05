use QL_Sach
go 
create table Account(
UserName varchar(20) primary key,
Password varchar(50)
)
go
insert into Account values('Admin', '12345678')
go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE Sp_Account_Login
	@UserName varchar(20),
	@Password varchar(50)
AS
BEGIN
	declare @count int
	declare @res bit
	select @count = count(*) from Account where UserName = @UserName and Password = @Password
	if @count > 0
		set @res = 1
	else
		set @res = 0
	select @res
END
go 
create table Book
(
	BookId INT IDENTITY(1,1) PRIMARY KEY,
	Title nvarchar(150),
	AuthorName nvarchar(150),
	Price float,
	Year int,
	CoverPage varchar(150) 
)
go
create table Chapter 
(
	ChapterId INT IDENTITY(1,1) PRIMARY KEY,
	ChapterName nvarchar(50),
	ShortContent nvarchar(250),
	BookId int,
	foreign key(BookId) references Book(BookId)
)
go
insert into Book values('MVC', 'Job', 210000, 2016, 'MVC_Book.png' )
go
insert into Book values('Mastering Java 9', 'Dr.Edward Lavieri, Peter Verhas', 140400, 2015, 'Java_Book.png' )
go
insert into Chapter values(N'Chương 2', 'MVC', 1)
go
insert into Chapter values(N'Chương 1', 'GUI Swing', 2)
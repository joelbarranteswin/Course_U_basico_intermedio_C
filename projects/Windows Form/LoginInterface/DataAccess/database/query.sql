create database MyCompany
go
use MyCompany
go
create table Users(
UserID int identity(1,1) primary key,
LoginName nvarchar (100) unique not null,
Password nvarchar (100) not null,
FirstName nvarchar(100) not null,
LastName nvarchar(100) not null,
Position nvarchar (100) not null,
Email nvarchar(150)not null
) 

insert into Users values ('admin','admin','Developer','Developer','Administrator','Support@Developer.cl')
insert into Users values ('Joel','barrantes','joel','barrantes','Engineer','joelbarrantespalacios@Developer.com')

SELECT * from Users WHERE LoginName='admin' AND Password='admin';

declare @user nvarchar(100)='admin'
declare @pass nvarchar(100)='admin'
SELECT * from Users WHERE LoginName=@user AND Password=@pass;

select * from Users;

DBCC CHECKIDENT (Users, RESEED, 0)
GO


DELETE FROM Users WHERE UserID BETWEEN 1 AND 10;
create database MVVMLoginDb
go
use MVVMLoginDb
go
create table [User]
(
	Id UNIQUEIDENTIFIER primary Key default NEWID(),
	Username nvarchar (50) unique not null,
	[Password] nvarchar (100) not null,
	[Name]nvarchar (50) not null,
	LastName nvarchar (50) not null,
	Email  nvarchar (100) unique not null
)
go
insert into [User] values (NEWID(), 'admin', 'admin','Joel','Barrantes', 'joelbarrantes.com')
insert into [User] values (NEWID(), 'admin', 'admin','Joel','Barrantes', 'joelbarrantes.com')
insert into [User] values (NEWID(), 'admin', 'admin','Joel','Barrantes', 'joelbarrantes.com')
go

select *from [User]
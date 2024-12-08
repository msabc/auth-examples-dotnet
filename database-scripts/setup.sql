use [master]
go

declare @dbname varchar(50) = 'AuthenticationExamples';

if not exists (select [name] from sys.databases where ('[' + [name] + ']' = @dbname OR [name] = @dbname))
begin
	create database AuthenticationExamples;
end
go

use AuthenticationExamples
go

create table dbo.[Role]
(
Id int primary key identity not null,
[Name] varchar(50) not null
)

create table dbo.[User]
(
Id int primary key identity not null,
Username varchar(50) not null,
Email varchar(100) not null,
PasswordHash varbinary(500) not null,
RoleId int foreign key references [dbo].[Role](Id) not null,
CreatedDate datetime not null default getdate(),
LastModifiedDate datetime not null default getdate()
)

go
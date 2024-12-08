use [master]
go

declare @dbname varchar(50) = 'AuthenticationExamples';

if not exists (select [name] from sys.databases where ('[' + [name] + ']' = @dbname OR [name] = @dbname))
throw 50001, 'The selected database does not exist. Please run the setup.sql script first.', 200;

use AuthenticationExamples
go

insert into [dbo].[Role] ([Name])
values
('Administrator'),
('User')
# About

A repository containing several authentication examples 
in .NET used for educational purposes.

The project connects to a **local** (for now) SQL Server database.

# Run the project locally

In order to run the project locally, you'll first need to set up the database. 

Assuming you have an instance of SQL Server installed:

- navigate to the 'database-scripts' folder
- run the '**setup.sql**' script
- run the '**data.sql**' script

> **Note**: The scripts need to be run in this exact order 
(because one creates the database and the other seeds it).

After you've successfully set up the database, you can just run '**BasicAuth.Api**' project.
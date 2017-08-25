# T4SSDT Generators

## Overview

This project is a collection of T4 text transformations that provide object oriented API to the content of 
Visual Studio database projects powered by [SQL Server Data Tools](https://docs.microsoft.com/en-us/sql/ssdt/download-sql-server-data-tools-ssdt).
As opposed to text templates available inside of the database project, these templates can be used in any other
project inside of the same solution. For example to generate POCO data model inside of the C# class library.

## Usage

Add `T4SSDT.ttinclude` file to the target Visual Studio project and make sure that template contains valid
paths to SSDT DLLs (usually located inside of the Visual Studio installation folder) and start using its APIs
in your own templates.

See `examples` folder to get an idea on how to use this project. [Model.tt](samples/Model/Model.tt) is a
sample POCO model generator for tables, views and user-defined table types. [Repository.tt](samples/Model/Repository.tt)
is a sample Repository pattern implementation generator in pure ADO.NET that generates CRUD operations, views 
and stored procedure calls.

Modify `Settings.ttinclude` to exclude certain database objects from code generation.

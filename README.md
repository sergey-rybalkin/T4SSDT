# T4SSDT Generators

## Overview

This project is a collection of T4 text transformations that provide object oriented API to the content of 
Visual Studio database projects powered by [SQL Server Data Tools]
(https://docs.microsoft.com/en-us/sql/ssdt/download-sql-server-data-tools-ssdt).
As opposed to text templates available inside of the database project these templates can be used in any other
project inside of the same solution. For example to generate POCO data model inside of the C# class library.

## Usage

Add content of the `src` folder to the target Visual Studio project and make sure that `T4SSDT.ttinclude`
template contains valid paths to SSDT DLLs (usually located inside of the Visual Studio installation folder).
Templates will try to locate database project in the active solution and run code generation based on the
available database schema. See `test` folder for examples.

Modify `Model.Settings.ttinclude` to exclude or modify database objects code generation configuration.
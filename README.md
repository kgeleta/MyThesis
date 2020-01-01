## Building

This steps are required only when changes in C# project were made. Otherwise go to Usage section

### Requirements:
- PowerShell 5.1 or later (for Linux users: [PowerShell Core 6.0](https://docs.microsoft.com/en-gb/powershell/scripting/install/installing-powershell-core-on-linux?view=powershell-6))
- dotnet 2.2 or later

Open PowerShell in cloned repository and run:
`./build.ps1`

To run unit tests before building run:
`./build.ps1 -test`

## Usage

Copy files from GoldBuild directory to your Unity project directory and open project.

### Requirements:
- PowerShell 5.1 or later (for Linux users: [PowerShell Core 6.0](https://docs.microsoft.com/en-gb/powershell/scripting/install/installing-powershell-core-on-linux?view=powershell-6))
- Unity 2019.2 or later
- SqlServer, PostgreSQL, MySQL or SQLite database

## Known issues

- Unity has problem with SQL Server Express - generating model classes will work but data won't be persisted. Use the full version of SQL Server or different database provider

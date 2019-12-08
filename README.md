## Building

### Requirements:
- PowerShell 5.1 or later (for Linux users: [PowerShell Core 6.0](https://docs.microsoft.com/en-gb/powershell/scripting/install/installing-powershell-core-on-linux?view=powershell-6))
- dotnet 2.2 or later

Open PowerShell in cloned repository and run:
`./build.ps1`

To run unit tests before building run:
`./build.ps1 -test`

## Known issues

- Unity has problem with SQL Server Express - generating model classes will work but data won't be persisted. Use full version of SQL Server or different database provider

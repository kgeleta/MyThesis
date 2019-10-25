param (
  [Parameter(Mandatory=$true, Position = 0)][string]$connectionString,
  [Parameter(Mandatory=$true, Position = 1)][ValidateSet('sqlServer','MySql','SQLite', 'PostgreSQL')][string]$provider
)

$exitCode = 0
Write-Host "Creating new project" -foregroundcolor green
mkdir Feedback
cd Feedback

try
{
  dotnet new classlib -f netcoreapp2.2
  Write-Host "Installing Entity Framework Core" -foregroundcolor green

  switch ($provider)
  {
    "sqlServer"
    {
      dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 2.2.6
      dotnet add package Microsoft.EntityFrameworkCore.Design -v 2.2.6

      Write-Host "Generating model classes" -foregroundcolor green
      dotnet ef dbcontext scaffold $connectionString Microsoft.EntityFrameworkCore.SqlServer -o Model
      break
    }
    "MySql"
    {
      dotnet add package MySql.Data.EntityFrameworkCore -v 8.0.18
      dotnet add package Microsoft.EntityFrameworkCore.Design -v 2.2.6

      Write-Host "Generating model classes" -foregroundcolor green
      dotnet ef dbcontext scaffold $connectionString MySql.Data.EntityFrameworkCore -o Model
      break
    }
    "SQLite"
    {
      dotnet add package Microsoft.EntityFrameworkCore.Sqlite -v 2.2.6
      dotnet add package Microsoft.EntityFrameworkCore.Design -v 2.2.6

      Write-Host "Generating model classes" -foregroundcolor green
      dotnet ef dbcontext scaffold $connectionString Microsoft.EntityFrameworkCore.Sqlite -o Model
      break
    }
    "PostgreSQL"
    {
      dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL -v 2.2.4
      dotnet add package Microsoft.EntityFrameworkCore.Design -v 2.2.6

      Write-Host "Generating model classes" -foregroundcolor green
      dotnet ef dbcontext scaffold $connectionString Npgsql.EntityFrameworkCore.PostgreSQL -o Model
      break
    }
  }
    # TODO: this should be atomic (if does not complete folder should be removed)

  Write-Host "Copying Model directory to Assets" -foregroundcolor green

  # this operation should be sqfe (provided that dotnet successfully generated project and Assets folder exists)
  cd ..
  Copy-Item -Path .\Feedback\Model -Destination .\Assets -Recurse -Force
}
catch
{
  $exitCode = 1
}
finally
{
  Write-Host "Removing Feedback directory" -foregroundcolor green
  Remove-Item .\Feedback -Recurse
  Write-Host "All done" -foregroundcolor green
  Exit $exitCode
}

param (
  [Parameter(Mandatory=$true, Position = 0)][string]$connectionString
)

Write-Host "Creating new project" -foregroundcolor green
mkdir Feedback
cd Feedback
dotnet new classlib -f netcoreapp2.2

Write-Host "Installing Entity Framework Core" -foregroundcolor green
dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 2.2.6
dotnet add package Microsoft.EntityFrameworkCore.Design -v 2.2.6

Write-Host "Generating model classes" -foregroundcolor green
dotnet ef dbcontext scaffold $connectionString Microsoft.EntityFrameworkCore.SqlServer -o Model

# TODO: this should be atomic (if does not complete folder should be removed)

Write-Host "Copying Model directory to Assets" -foregroundcolor green
cd ..
Copy-Item -Path .\Feedback\Model -Destination .\Assets -Recurse

Write-Host "Removing Feedback directory" -foregroundcolor green
Remove-Item .\Feedback -Recurse

Write-Host "All done" -foregroundcolor green

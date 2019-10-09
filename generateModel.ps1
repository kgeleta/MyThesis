param (
  [Parameter(Mandatory=$true, Position = 0)][string]$connectionString
)


mkdir Model
cd Model
dotnet new classlib -f netcoreapp2.2
dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 2.2.6
dotnet add package Microsoft.EntityFrameworkCore.Design -v 2.2.6

dotnet ef dbcontext scaffold $connectionString Microsoft.EntityFrameworkCore.SqlServer -o Model1

# TODO: this should be atomic (if does not complete folder should be removed)

  param (
    [Parameter(Mandatory=$false, Position = 0)][string]$path,
    [Parameter(Mandatory=$false)][switch]$test
  )

  # install dependencies
  Write-Host "Installing dependencies" -foregroundcolor green
  .paket\paket.exe install

  # run unit test
  if($test -eq $true)
  {
    Write-Host "Running unit tests" -foregroundcolor green
#    dotnet build "UnityFeedbackTest\UnityFeedbackTest.csproj"
    dotnet test "UnityFeedbackTest\UnityFeedbackTest.csproj"
  }

  # build project
  if($path)
  {
    Write-Host "Building project to specified location: $path" -foregroundcolor green
    dotnet build "UnityFeedback\UnityFeedback.csproj" -c Release -f net461 -o $($path)
  } else{
    Write-Host "Building project" -foregroundcolor green
    dotnet build "UnityFeedback\UnityFeedback.csproj"
  }

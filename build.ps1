  param (
    [Parameter(Mandatory=$true, Position = 0)][string]$path,
    [Parameter(Mandatory=$false)][switch]$test
  )

  # run unit test
  if($test -eq $true)
  {
    Write-Host "Running unit tests" -foregroundcolor green
#    dotnet build "UnityFeedbackTest\UnityFeedbackTest.csproj"
    dotnet test "UnityFeedbackTest\UnityFeedbackTest.csproj"
  }

  # build project
  Write-Host "Building project to specified location: $path" -foregroundcolor green
  dotnet build "UnityFeedback\UnityFeedback.csproj" -c Debug -f net461 -o "$($path)\Assets\Plugins"

  # remove unityEditor.dll from output folder
  Remove-Item "$($path)\Assets\Plugins\UnityEngine.dll"

  # move powershell script to project
  Copy-Item -Path .\generateModel.ps1 -Destination "$($path)\PowerShell" -Force

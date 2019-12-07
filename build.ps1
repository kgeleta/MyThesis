  param (
    [Parameter(Mandatory=$false)][switch]$test
  )

  # run unit test
  if($test -eq $true)
  {
    Write-Host "Running unit tests" -foregroundcolor green
    dotnet test "UnityFeedbackTest\UnityFeedbackTest.csproj"
  }

  # build project
  Write-Host "Building project" -foregroundcolor green
  dotnet build "UnityFeedback\UnityFeedback.csproj" -c Release

  Move-Item -Path UnityFeedback\bin\Release\UnityFeedback.dll -Destination GoldBuild\UnityFeedback.dll -Force
  Move-Item -Path UnityFeedback\bin\Release\I18N.dll -Destination GoldBuild\I18N.dll -Force
  Move-Item -Path UnityFeedback\bin\Release\I18N.West.dll -Destination GoldBuild\I18N.West.dll -Force

  Write-Host "Merging assemblies" -foregroundcolor green
  packages\ILMerge.3.0.29\tools\net452\ILMerge.exe /allowDup /wildcards /out:GoldBuild\FeedbackDependencies.dll UnityFeedback\bin\Release\*.dll
 # -o "$($path)\Assets\Plugins"
  # remove unityEditor.dll from output folder
  # Remove-Item "$($path)\Assets\Plugins\UnityEngine.dll"

  # move powershell script to project
  # Copy-Item -Path .\generateModel.ps1 -Destination "$($path)\PowerShell" -Force

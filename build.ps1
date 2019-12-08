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

  Move-Item -Path UnityFeedback\bin\Release\UnityFeedback.dll -Destination GoldBuild\Plugins\UnityFeedback.dll -Force
  Move-Item -Path UnityFeedback\bin\Release\I18N.dll -Destination GoldBuild\Plugins\I18N.dll -Force
  Move-Item -Path UnityFeedback\bin\Release\I18N.West.dll -Destination GoldBuild\Plugins\I18N.West.dll -Force

  Write-Host "Merging assemblies" -foregroundcolor green
  packages\ILMerge.3.0.29\tools\net452\ILMerge.exe /allowDup /wildcards /out:GoldBuild\Plugins\FeedbackDependencies.dll UnityFeedback\bin\Release\*.dll

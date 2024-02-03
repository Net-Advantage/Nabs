# Set console colors to defaults
$Host.UI.RawUI.ForegroundColor = 'White'
$Host.UI.RawUI.BackgroundColor = 'Black'
Clear-Host

# Define paths for coverage reports and test results
$rootDirectory = Get-Location
$testResultsPath = "TestResults"
$coverageReportPath = "coveragereport"

# Clean up old test results and coverage reports
Get-ChildItem -Path $rootDirectory -Recurse -Filter $testResultsPath -Directory | ForEach-Object {
    Remove-Item -Path $_.FullName -Recurse -Force
    Write-Host "Removed TestResults directory at: $($_.FullName)"
}

if (Test-Path $coverageReportPath) {
    Remove-Item -Path $coverageReportPath -Recurse -Force
}

dotnet restore src/Nabs.sln --configfile ./src/nuget.config
dotnet build src/Nabs.sln --configuration Release --no-restore
dotnet test src/Nabs.sln --configuration Release --no-restore --no-build --logger "console;verbosity=detailed" --settings src/coverlet.runsettings
& "$env:UserProfile/.nuget/packages/reportgenerator/5.2.0/tools/net8.0/ReportGenerator.exe" -reports:"**/TestResults/*/coverage.cobertura.xml" -targetdir:"coveragereport"
dotnet pack src/Nabs.sln --configuration Release --no-restore --no-build --output nupkgs /p:PackageVersion=8.0.0

# Open coverage report in browser
Start-Process -FilePath (Join-Path -Path $coverageReportPath -ChildPath "index.html")
$Host.UI.RawUI.ForegroundColor = 'White'
$Host.UI.RawUI.BackgroundColor = 'Black'
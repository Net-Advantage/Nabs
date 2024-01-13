dotnet restore src/Nabs.sln --configfile ./src/nuget.config
dotnet build src/Nabs.sln --configuration Release --no-restore
dotnet test src/Nabs.sln --configuration Release --no-restore --no-build --logger "console;verbosity=detailed" --settings src/coverlet.runsettings
dotnet test src/Nabs.sln --settings src/coverlet.runsettings
& "$env:UserProfile/.nuget/packages/reportgenerator/5.2.0/tools/net8.0/ReportGenerator.exe" -reports:"**/TestResults/*/coverage.cobertura.xml" -targetdir:"coveragereport"
dotnet pack src/Nabs.sln --configuration Release --no-restore --no-build --output nupkgs /p:PackageVersion=8.0.0

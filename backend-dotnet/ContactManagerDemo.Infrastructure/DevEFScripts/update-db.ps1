$projectRoot = Resolve-Path "../.."
Set-Location $projectRoot

$dotnetEfDatabaseUpdate = "dotnet ef database update --context AppDataContext --project ./ContactManagerDemo.Infrastructure --startup-project ./ContactManagerDemo.RestApi"
Write-Host "Running: $dotnetEfDatabaseUpdate"
Invoke-Expression $dotnetEfDatabaseUpdate

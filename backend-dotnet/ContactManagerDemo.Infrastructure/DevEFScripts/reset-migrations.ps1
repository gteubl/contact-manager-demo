$projectRoot = Resolve-Path "../.."
Set-Location $projectRoot
Write-Host "Changed to $projectRoot"

Write-Host "Current directory: $(Get-Location)"

$MigrationsPath = "./ContactManagerDemo.Infrastructure/Migrations"

if (Test-Path $MigrationsPath) {
    Remove-Item -Recurse -Force $MigrationsPath
    Write-Host "Deleted $MigrationsPath"
} else {
    Write-Host "$MigrationsPath does not exist"
}

$dotnetEfMigrationsAdd = "dotnet ef migrations add InitialCreate --context AppDataContext --output-dir Migrations --project ./ContactManagerDemo.Infrastructure --startup-project ./ContactManagerDemo.RestApi"
Write-Host "Running: $dotnetEfMigrationsAdd"
Invoke-Expression $dotnetEfMigrationsAdd

$dotnetEfDatabaseUpdate = "dotnet ef database update --context AppDataContext --project ./ContactManagerDemo.Infrastructure --startup-project ./ContactManagerDemo.RestApi"
Write-Host "Running: $dotnetEfDatabaseUpdate"
Invoke-Expression $dotnetEfDatabaseUpdate

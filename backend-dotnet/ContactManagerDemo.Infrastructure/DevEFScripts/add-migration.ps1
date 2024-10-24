# Change to the project root directory
$projectRoot = Resolve-Path "../.."
Set-Location $projectRoot

# Prompt for the migration name
$migrationName = Read-Host "Enter the migration name"

# Step 2: Run dotnet ef migrations add with the provided migration name
$dotnetEfMigrationsAdd = "dotnet ef migrations add $migrationName --context ServiFacileDataContext --output-dir Migrations --project ./Infrastructure --startup-project ./ProvidersRestApi"
Write-Host "Running: $dotnetEfMigrationsAdd"
Invoke-Expression $dotnetEfMigrationsAdd

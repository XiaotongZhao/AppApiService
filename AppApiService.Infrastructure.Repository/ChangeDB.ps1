Set-Location -Path "../AppApiService"

# Set migration name
$migrationName = "ChangeTablePipeline"

# Set project path
$projectPath = "../AppApiService.Infrastructure.Repository"

# add-Migration
dotnet ef migrations add $migrationName --project $projectPath
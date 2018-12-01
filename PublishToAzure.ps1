param(
    [Parameter(Position = 0, Mandatory = $true)]
    [ValidateNotNullOrEmpty()]
    [System.String]
    $projectPath,

    [Parameter(Position = 1, Mandatory = $true)]
    [ValidateNotNullOrEmpty()]
    [System.String]
    $configPath,

    [Parameter(Position = 2, Mandatory = $true)]
    [ValidateNotNullOrEmpty()]
    [System.String]
    $azureGroup
)

$projectToPublish = "$projectPath\$projectPath.csproj"
$zipPath = "build/zip"
$zipFile = "$zipPath/release.zip"
$publishPath = "/vs2017/fitnesstrackerms/build/publish"

dotnet publish $projectPath -c release -o $publishPath
Copy-Item "$($configPath)/*.*" -Destination $publishPath
Compress-Archive -Path $publishPath/* -DestinationPath $($zipFile)
az webapp deployment source config-zip --resource-group $azureGroup --name $azureGroup --src $($zipFile)
Remove-Item $publishPath/*.* -Force -Recurse
Remove-Item $zipFile

#*****EXAMPLES*******

# WORKOUT SERVICE
#.\PublishToAzure.ps1 FitnessTracker.Workout.Service configpath/workoutservice/azure/configfiles FTWorkoutAPI

# DIET SERVICE
#.\PublishToAzure.ps1 FitnessTracker.Diet.Service configpath/dietservice/azure/configfiles FTDietAPI

# WEB STATUS
#.\PublishToAzure.ps1 FitnessTracker.Presentation.WebStatus configpath/webstatus/azure/configfiles FTWebStatus

# Angular
#NOTE: Copy the DIST folder to the publish folder (D:\VS2017\FitnessTrackerMS\build\publish) prior to running the command below
#.\PublishToAzure.ps1 FitnessTracker.Presentation.Angular configpath/fitnesstracker/azure/configfiles FTAngular
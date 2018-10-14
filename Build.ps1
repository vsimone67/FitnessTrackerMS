Param([Parameter(Mandatory=$true)][string[]]$images)

foreach ($image in $images) {        
     docker-compose build $image # use docker-compose file to build specifc image
     & .\PushImage $image  # user powershell script to move the newly built image to dockerhub
   }
  
  
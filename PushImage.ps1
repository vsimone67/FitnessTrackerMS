Param([Parameter(Mandatory=$true)][string]$image)

docker tag $image vsimone67/$($image):latest
docker push vsimone67/$($image):latest
param(
    [Parameter(Position = 0)]
    [ValidateNotNull()]
    [System.Boolean]
    $prod = $false
)
# To deploy to prod: .\deploysite.ps1 -prod $true
# To deploy to test: .\deploysite.ps1

$appdirectory = "D:\\VS2017\\FitnessTrackerMS\\configpath\fitnesstracker\\dist"
$createFolders = $false

# Test Envionrment
# Site: https://fitnesstrackerv2.azurewebsites.net/
$usernameTest = 'FitnessTrackerv2\$FitnessTrackerv2'
$passwordTest = "B1ApyDdD7ByC24SrPxKg7C0pQXxvLkpdrsYn9AH8n0mufPiLluhapxTLY8X3"
$urlTest = "ftp://waws-prod-blu-019.ftp.azurewebsites.windows.net/site/wwwroot" 


# Production Envionrment
# Site: http://vsfitnesstracker.azurewebsites.net/
$usernameProd = 'VSFitnessTracker\$VSFitnessTracker'
$passwordProd = "5XA7GFEWXewiPtpCF52Qqmmqtiyj3NjMcojCLTAjM6ob61b3aT0tmscS15a1"
$urlProd = "ftp://waws-prod-blu-019.ftp.azurewebsites.windows.net/site/wwwroot" 

#Program Variables
$username = ""
$password = ""
$url = ""

function Main() {

    if ($createFolders) {
        "Creating Folders on FTP Site"
        CreateFoldersOnSite
    }

    if ($prod) {
        "Deploying to Prod..."
        $username = $usernameProd
        $password = $passwordProd
        $url = $urlProd
    }
    else {
        "Deploying to Test..."
        $username = $usernameTest
        $password = $passwordTest
        $url = $urlTest
    }

    RunBuild
    UploadFile

    "Deploy Completed..."
}

function CreateFoldersOnSite() {

    $folders = Get-ChildItem -Path $appdirectory -Recurse| where {$_.Attributes -match 'Directory'}

	
    foreach ($folder in $folders) {    


        $pos = $folder.FullName.IndexOf('wwwroot')

        $DesFolder = $folder.FullName.Substring($pos + 8)
        $DesFolder = $url + "/" + $DesFolder
   
        "Creating Folder " + $DestFolder
  
        try {
            $makeDirectory = [System.Net.WebRequest]::Create($DesFolder);
            $makeDirectory.Credentials = New-Object System.Net.NetworkCredential($username, $password);
            $makeDirectory.Method = [System.Net.WebRequestMethods+FTP]::MakeDirectory;
            $makeDirectory.GetResponse();
            #folder created successfully
        }
        catch [Net.WebException] {
            try {
                #if there was an error returned, check if folder already existed on server
                $checkDirectory = [System.Net.WebRequest]::Create($DesFolder);
                $checkDirectory.Credentials = New-Object System.Net.NetworkCredential($username, $password);
                $checkDirectory.Method = [System.Net.WebRequestMethods+FTP]::PrintWorkingDirectory;
                $response = $checkDirectory.GetResponse();
                #folder already exists!
            }
            catch [Net.WebException] {
                #if the folder didn't exist
            }
        }
    }

	
}

function RunBuild() {
  
    "Compiling Code..."
    Start-Process ng build -Wait -NoNewWindow
 
}

function UploadFile() {
    # Upload files recursively 
    Set-Location $appdirectory
    $webclient = New-Object -TypeName System.Net.WebClient
    $webclient.Credentials = New-Object System.Net.NetworkCredential($username, $password)
    # https://www.kittell.net/code/powershell-ftp-upload-directory-sub-directories/      
    $files = Get-ChildItem -Path $appdirectory -Recurse | Where-Object {!($_.PSIsContainer)}
    
    foreach ($file in $files) {
        $relativepath = (Resolve-Path -Path $file.FullName -Relative).Replace(".\", "").Replace('\', '/')
        $uri = New-Object System.Uri("$url/$relativepath")
        "Uploading to " + $uri.AbsoluteUri
	
        $webclient.UploadFile($uri, $file.FullName)
    } 

    $webclient.Dispose()

}

#Start program here
Main

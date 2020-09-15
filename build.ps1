[CmdletBinding()]
param(

)

Push-Location -Path "./src/"

try {
    dotnet clean
    dotnet publish /property:PublishWithAspNetCoreTargetManifest=false
    #Start-Process -FilePath "dotnet" -ArgumentList @("clean") -Wait -NoNewWindow -ErrorAction Stop
    #Start-Process -FilePath "dotnet" -ArgumentList @("publish", "/property:PublishWithAspNetCoreTargetManifest=false") -Wait -NoNewWindow -ErrorAction Stop
}
finally {
    Pop-Location
}

if (Test-Path -Path "./build") {
    Remove-Item -Path "./build" -Recurse -Force
}

$null = New-Item -Type Directory -Path "./build"
$null = New-Item -Type Directory -Path "./build/pwsh-azuresentinel"

Copy-Item -Path "./src/bin/Debug/netstandard2.1/publish/AzSentinel.dll" -Destination "./build/pwsh-azuresentinel/"
Copy-Item -Path "./src/bin/Debug/netstandard2.1/publish/Microsoft.Identity.Client.dll" -Destination "./build/pwsh-azuresentinel/"
Copy-Item -Path "./src/bin/Debug/netstandard2.1/publish/System.Text.Json.dll" -Destination "./build/pwsh-azuresentinel/"

Copy-Item -Path "./module-manifest/pwsh-azuresentinel.psd1" -Destination "./build/pwsh-azuresentinel/"
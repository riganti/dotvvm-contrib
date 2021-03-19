Param(
    [string]$version,
    [string]$server, 
    [string]$internalServer, 
    [string]$apiKey,
    [string]$packageId
)

    & .\tools\nuget.exe install $packageId -OutputDirectory .\tools\packages -version $version -DirectDownload -NoCache -DependencyVersion Ignore -source $internalServer
    $nupkgFile = dir -s ./tools/packages/$packageId.$version.nupkg | Select -First 1

    Write-Host "Package downloaded from '$internalServer'."

    Write-Host "Uploading package..."
    & .\Tools\nuget.exe push $nupkgFile -source $server -apiKey $apiKey
    Write-Host "Package uploaded to $server."

    Remove-Item $nupkgFile
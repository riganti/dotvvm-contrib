Param(
    [string]$version,
    [string]$server, 
    [string]$internalServer, 
    [string]$apiKey,
    [string]$packageIds
)

foreach ($packageId in $packageIds.Split(',')) { 

	$packageId = "DotVVM.Contrib." + $packageId.Trim()

	& .\tools\nuget.exe install $packageId -OutputDirectory .\tools\packages -version $version -DirectDownload -NoCache -DependencyVersion Ignore -source $internalServer
	$nupkgFile = dir -s ./tools/packages/$packageId.$version.nupkg | Select -First 1
	Write-Host "Downloaded package located on '$nupkgFile'" 

	Write-Host "Uploading package..."
	& .\Tools\nuget.exe push $nupkgFile -source $server -apiKey $apiKey
	Write-Host "Package uploaded to $server."

	Remove-Item $nupkgFile

}
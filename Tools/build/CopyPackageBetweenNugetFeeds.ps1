Param(
    [string]$version,
    [string]$server, 
    [string]$internalServer, 
    [string]$apiKey,
    [string]$controlName
)

### Configuration

$packages = @()

if ($controlName -eq "") {
	# add all controls 
	$controlDirs = Get-ChildItem Controls/ -Directory | Where-Object { $_.Name.StartsWith("_") -eq $false } | Select-Object Name
	foreach ($controlDir in $controlDirs) {
		$packages += @([pscustomobject]@{ Package = "DotVVM.Contrib.$($controlDir.Name)"; Directory = "Controls\$($controlDir.Name)\src\DotVVM.Contrib.$($controlDir.Name)" })
	}
} else {
	# split comma-separated controls
	foreach ($controlDir in $controlName.Split(',')) {
		$controlDir = $controlDir.Trim()
		$packages += @([pscustomobject]@{ Package = "DotVVM.Contrib." + $controlDir; Directory = "Controls\" + $controlDir + "\src\DotVVM.Contrib." + $controlDir })
	}
}

foreach ($package in $packages) { 

	$packageId = $package.Package

	& .\tools\nuget.exe install $packageId -OutputDirectory .\tools\packages -version $version -DirectDownload -NoCache -DependencyVersion Ignore -source $internalServer
	$nupkgFile = dir -s ./tools/packages/$packageId.$version.nupkg | Select -First 1
	Write-Host "Downloaded package located on '$nupkgFile'" 

	Write-Host "Uploading package..."
	& .\Tools\nuget.exe push $nupkgFile -source $server -apiKey $apiKey
	Write-Host "Package uploaded to $server."

	Remove-Item $nupkgFile

}
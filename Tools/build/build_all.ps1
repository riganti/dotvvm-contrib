$dirs = Get-ChildItem ../../Controls/ -Directory | Where-Object { $_.Name.StartsWith("_") -eq $false } | Select-Object Name
foreach ($dir in $dirs) {
 
	pushd ../../Controls/$($dir.Name)/src/DotVVM.Contrib
	
	$csproj = get-childitem -filter *.csproj
	
	$output = (& dotnet build $($csproj[0].Name) | out-string)
	if ($LastExitCode -ne 0) {
		write-host "Error building $($dir.Name)"		
	} else {
		write-host "Success building $($dir.Name)"
	}
	
	popd
 
	#set-content -path "$($dir.Name).log" -value $output
}
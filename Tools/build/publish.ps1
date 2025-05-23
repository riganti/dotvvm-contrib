param([String]$version, [String]$controlName, [String]$apiKey, [String]$server, [String]$nugetRestoreAltSource = "")

Write-Host ">> : $pwd";

### Helper Functions

function Invoke-Git {
<#
.Synopsis
Wrapper function that deals with Powershell's peculiar error output when Git uses the error stream.

.Example
Invoke-Git ThrowError
$LASTEXITCODE

#>
    [CmdletBinding()]
    param(
        [parameter(ValueFromRemainingArguments=$true)]
        [string[]]$Arguments
    )

    & {
        [CmdletBinding()]
        param(
            [parameter(ValueFromRemainingArguments=$true)]
            [string[]]$InnerArgs
        )
        git.exe $InnerArgs 2>&1
    } -ErrorAction SilentlyContinue -ErrorVariable fail @Arguments

    if ($fail) {
        $fail.Exception
    }
}

function CleanOldGeneratedPackages() {
    Write-Host "CleanOldGeneratedPackages started"
	foreach ($package in $packages) {
		del .\$($package.Directory)\bin\debug\*.nupkg -ErrorAction SilentlyContinue
	}
}

function SetVersion() {
    Write-Host "SetVersion started"
  	foreach ($package in $packages) {
		
		Write-Host "$pwd\$($package.Directory)"
		$csproj = get-childitem "$pwd\$($package.Directory)" -filter *.csproj		
		
		$filePath = $csproj[0].FullName
		$file = [System.IO.File]::ReadAllText($filePath, [System.Text.Encoding]::UTF8)
		$file = [System.Text.RegularExpressions.Regex]::Replace($file, "\<VersionPrefix\>([^<]+)\</VersionPrefix\>", "<VersionPrefix>" + $version + "</VersionPrefix>")
		$file = [System.Text.RegularExpressions.Regex]::Replace($file, "\<PackageVersion\>([^<]+)\</PackageVersion\>", "<PackageVersion>" + $version + "</PackageVersion>")
		[System.IO.File]::WriteAllText($filePath, $file, [System.Text.Encoding]::UTF8)
		
		$filePath = "$pwd\$($package.Directory)\Properties\AssemblyInfo.cs"
		$file = [System.IO.File]::ReadAllText($filePath, [System.Text.Encoding]::UTF8)
		$file = [System.Text.RegularExpressions.Regex]::Replace($file, "\[assembly: AssemblyVersion\(""([^""]+)""\)\]", "[assembly: AssemblyVersion(""" + $versionWithoutPre + """)]")
		$file = [System.Text.RegularExpressions.Regex]::Replace($file, "\[assembly: AssemblyFileVersion\(""([^""]+)""\)]", "[assembly: AssemblyFileVersion(""" + $versionWithoutPre + """)]")
		[System.IO.File]::WriteAllText($filePath, $file, [System.Text.Encoding]::UTF8)
	}  
}

function BuildPackages() {
    Write-Host "BuildPackages started"
	foreach ($package in $packages) {
		cd .\$($package.Directory)
        Write-Host "Build directory: $pwd";
		if ($nugetRestoreAltSource -eq "") {
			& dotnet restore | Out-Host
		}
		else {
			& dotnet restore --source $nugetRestoreAltSource --source "https://nuget.org/api/v2/" | Out-Host
		}

        Write-Host "dotnet pack started at: $pwd";

		& dotnet pack -c Release| Out-Host
		cd ..\..\..\..
        Write-Host ">> : $pwd";
	}
}

function PushPackages() {
Write-Host "Pushing the packages to feed.";
	foreach ($package in $packages) {
		& .\Tools\nuget.exe push .\$($package.Directory)\bin\release\$($package.Package).$version.nupkg -source $server -apiKey $apiKey | Out-Host
	}
}


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

### Publish Workflow

$versionWithoutPre = $version
if ($versionWithoutPre.Contains("-")) {
	$versionWithoutPre = $versionWithoutPre.Substring(0, $versionWithoutPre.IndexOf("-"))
}

CleanOldGeneratedPackages;
SetVersion;
BuildPackages;
PushPackages;

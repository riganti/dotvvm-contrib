param([String]$version,[String]$dotVVMVersion, [String]$apiKey, [String]$server, [String]$branchName, [String]$repoUrl, [String[]] $exludeControls = "", [String]$nugetRestoreAltSource = "")


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
	foreach ($package in $packages) {
		del .\$($package.Directory)\DotVVM.Contrib\bin\debug\*.nupkg -ErrorAction SilentlyContinue
	}
}

function SetVersion() {
  	foreach ($package in $packages) {

        $folders = Get-ChildItem $package.Directory -Directory | Where { $_.Name.Contains('DotVVM.Contrib') -and !$_.Name.Contains('DotVVM.Contrib.Tests') } |  Foreach { [pscustomobject]@{ Package = $_.Name; Directory = $package.Directory +"\" +  $_.Name}} ;

        foreach ($folder in $folders){

            if($folder.Package -eq 'DotVVM.Contrib'){
                $filePath = ".\$($folder.Directory)\" + $folder.Package + ".csproj";
		        $file = [System.IO.File]::ReadAllText($filePath, [System.Text.Encoding]::UTF8)
		        $file = [System.Text.RegularExpressions.Regex]::Replace($file, "\<VersionPrefix\>([^<]+)\</VersionPrefix\>", "<VersionPrefix>" + $version + "</VersionPrefix>")
		        $file = [System.Text.RegularExpressions.Regex]::Replace($file, "\<PackageVersion\>([^<]+)\</PackageVersion\>", "<PackageVersion>" + $version + "</PackageVersion>")
                $file = [System.Text.RegularExpressions.Regex]::Replace($file, "\<TargetFrameworks\>([^<]+)\</TargetFrameworks\>", "<TargetFrameworks>netcoreapp2.0;net451</TargetFrameworks>")
		        [System.IO.File]::WriteAllText($filePath, $file, [System.Text.Encoding]::UTF8)
		
		        $filePath = ".\$($package.Directory)\DotVVM.Contrib\Properties\AssemblyInfo.cs"
		        $file = [System.IO.File]::ReadAllText($filePath, [System.Text.Encoding]::UTF8)
		        $file = [System.Text.RegularExpressions.Regex]::Replace($file, "\[assembly: AssemblyVersion\(""([^""]+)""\)\]", "[assembly: AssemblyVersion(""" + $versionWithoutPre + """)]")
		        $file = [System.Text.RegularExpressions.Regex]::Replace($file, "\[assembly: AssemblyFileVersion\(""([^""]+)""\)]", "[assembly: AssemblyFileVersion(""" + $versionWithoutPre + """)]")
		        [System.IO.File]::WriteAllText($filePath, $file, [System.Text.Encoding]::UTF8)
            }
            else{
                $filePath = ".\$($folder.Directory)\" + $folder.Package + ".csproj";
		        $file = [System.IO.File]::ReadAllText($filePath, [System.Text.Encoding]::UTF8)
                $file = [System.Text.RegularExpressions.Regex]::Replace($file, "\<TargetFramework\>([^<]+)\</TargetFramework\>", "<TargetFramework>netcoreapp2.0</TargetFramework>")                $file = [System.Text.RegularExpressions.Regex]::Replace($file, "\<PackageReference Include=""DotVVM.AspNetCore"" Version=""([^<]+)\"" />", " <PackageReference Include=""DotVVM.AspNetCore"" Version=""$dotVVMVersion""/>")                $file = [System.Text.RegularExpressions.Regex]::Replace($file, "\<PackageReference Include=""Microsoft.AspNetCore"" Version=""([^<]+)\"" />", " <PackageReference Include=""Microsoft.AspNetCore.All"" Version=""2.0.0""/>")                $file = [System.Text.RegularExpressions.Regex]::Replace($file, "\<PackageReference Include=""Microsoft.AspNetCore.StaticFiles"" Version=""([^<]+)\"" />", " <PackageReference Include=""Microsoft.AspNetCore.StaticFiles"" Version=""2.0.0""/>")		        [System.IO.File]::WriteAllText($filePath, $file, [System.Text.Encoding]::UTF8)
            }

        
       
        }
		
	}  
}

function BuildPackages() {
	foreach ($package in $packages) {

        $folders = Get-ChildItem $package.Directory -Directory | Where { $_.Name -eq 'DotVVM.Contrib'}  | Foreach { [pscustomobject]@{ Package = $_.Name; Directory = $package.Directory +"\" +  $_.Name}} ;

        foreach($folder in $folders){
		    cd .\$($folder.Directory)
		
		    if ($nugetRestoreAltSource -eq "") {
			    & dotnet restore | Out-Host
		    }
		    else {
			    & dotnet restore --source $nugetRestoreAltSource --source https://nuget.org/api/v2/ | Out-Host
		    }
		
		    & dotnet pack | Out-Host

		    cd ..\..\..\..
        }
	}
}

function Build(){
    foreach ($package in $packages) {
       $folders = Get-ChildItem $package.Directory -Directory | Where {$_.Name.Contains('DotVVM.Contrib')}| Foreach { [pscustomobject]@{ Package = $_.Name; Directory = $package.Directory +"\" +  $_.Name}};

       foreach($folder in $folders){
         cd .\$($folder.Directory)
         dotnet build
         cd ..\..\..\..
       }
    }
}

function PushPackages() {
	foreach ($package in $packages) {
		& .\Tools\nuget.exe push .\$($package.Directory)\DotVVM.Contrib\bin\debug\$($package.Package).$version.nupkg -source $server -apiKey $apiKey | Out-Host
	}
}

function GitCheckout() {
	invoke-git checkout $branchName
	invoke-git -c http.sslVerify=false pull $repoUrl $branchName
}

function GitPush() {
	invoke-git commit -am "Controls: NuGet packages version $version"
	invoke-git rebase HEAD $branchName
	invoke-git push --follow-tags $repoUrl $branchName
}



### Configuration

$packages = Get-ChildItem .\Controls -Directory |Where {$_.Name -match '^(?!_)' -and ($exludeControls.Count -gt 0 -and !$exludeControls.Contains($_.Name))} | Foreach { [pscustomobject]@{ Package = "DotVVM.Contrib." + $_.Name; Directory = "Controls\" + $_.Name + "\src"}}

### Publish Workflow

$versionWithoutPre = $version
if ($versionWithoutPre.Contains("-")) {
	$versionWithoutPre = $versionWithoutPre.Substring(0, $versionWithoutPre.IndexOf("-"))
}

CleanOldGeneratedPackages;
GitCheckout;
SetVersion;
BuildPackages;
Build;
PushPackages;
GitPush;

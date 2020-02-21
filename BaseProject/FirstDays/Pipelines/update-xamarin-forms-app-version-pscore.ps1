<#
.SYNOPSIS
	Sets up Xamarin Forms app version (c) 2019 Plain Concepts
.DESCRIPTION
    This script sets up Xamarin Forms app version for different platforms.
    Can be useful to use locally, or inside an automated build environment.
.EXAMPLE
	.\<script_name> -Android -MajorVersion 2 -MinorVersion 288 -PathToAndroidManifest <...>\AndroidManifest.xml
.LINK
	https://dev.azure.com/plainconcepts
#>

param
(
  [Parameter(HelpMessage="Specify 'Android' platform. Aditionally, you must define 'PathToAndroidManifest' parameter")][switch][bool]$Android = $false,
  [Parameter(HelpMessage="Specify 'iOS' platform. Aditionally, you must define 'PathToIOSInfoPlist' and 'PathToIOSAssemblyInfo' parameters")][switch][bool]$iOS = $false,
  [Parameter(Mandatory=$true)][string]$AppVersion,
  [Parameter(Mandatory=$true)][string]$StringAppVersion,
  [string]$PathToAndroidManifest,
  [string]$PathToIOSInfoPlist,
  [string]$PathToIOSAssemblyInfo
)

## --------------------------------------------
## - NOTE: MAIN SCRIPT AT THE END OF THE FILE -
## --------------------------------------------

##### UTILITY FUNCTIONS #################################

function LogError($line) { Write-Host "##[error] $line" -Foreground Red -Background Black }
function LogWarning($line) { Write-Host "##[warning] $line" -Foreground DarkYellow -Background Black }
function LogDebug($line) { Write-Host "##[debug] $line" -Foreground Blue -Background Black }

##########################################################

function SetAndroidVersion([string]$PathToAndroidManifest, [string]$VersionCode, [string]$VersionName) {

    if ($null -eq $PathToAndroidManifest -Or $PathToAndroidManifest -eq '') {
        LogError "Android Manifest path not set. Exiting..."
        return
    }

    # Debug
    Write-Host "Path to Android Manifest: $PathToAndroidManifest"
    Write-Host "versionCode: $VersionCode"
    Write-Host "versionName: $VersionName"

    if (-Not (Test-Path $PathToAndroidManifest)) {
        LogError "Android Manifest not found on $PathToAndroidManifest. Exiting.."
        return
    }

    LogDebug "START updating AndroidManifest on $PathToAndroidManifest, will set versionName $VersionName, versionCode $VersionCode"
    $FileXml = [xml] (Get-Content $PathToAndroidManifest )
    $ManifestVersionName = Select-Xml -xml $FileXml -Xpath "/manifest/@android:versionName" -namespace @{android="http://schemas.android.com/apk/res/android"}
    $ManifestVersionName.Node.Value = $VersionName
    $ManifestVersionCode = Select-Xml -xml $FileXml -Xpath "/manifest/@android:versionCode" -namespace @{android="http://schemas.android.com/apk/res/android"}
    $ManifestVersionCode.Node.Value = $VersionCode
    $FileXml.Save($PathToAndroidManifest)
    LogDebug "END updating AndroidManifest"
}

function SetIOSVersion([string] $PathToPlist, [string] $PathToAssembly, [string]$BundleShortVersion, [string]$BundleVersion) {

    if ($null -eq $PathToPlist -Or $PathToPlist -eq '' -Or $null -eq $PathToAssembly -Or $PathToAssembly -eq '') {
        LogError "iOS paths to Info.plist and AssemblyInfo must be set. Exiting..."
        return
    }

    # Debug
    Write-Host "Path to Info.plist: $PathToPlist"
    Write-Host "Path to assembly: $PathToAssembly"
    Write-Host "Bundle short version: $BundleShortVersion"
    Write-Host "Bundle version: $BundleVersion"

    if (-Not (Test-Path $PathToPlist)) {
        LogError "Info.plist not found on $PathToPlist. Exiting..."
        return
    }

    # Apple wants xxx.xxx.xxx
    LogDebug "START updating Info.plist in $PathToPlist, will set shortVersion $BundleShortVersion, version $BundleVersion"
    & plutil -replace CFBundleShortVersionString -string $BundleShortVersion $PathToPlist
    & plutil -replace CFBundleVersion -string $BundleVersion $PathToPlist
    LogDebug "END updating Info.plist"

    # NOTE: AssemblyInfo doesn't need to exist previously
    LogDebug "START updating AssemblyInfo in $PathToAssembly, will set AssemblyVersion $BundleShortVersion"

    if (-Not (Test-Path $PathToAssembly)) {

        LogDebug "File doesn't exists, creating it..."
        New-Item -Path $PathToAssembly -ItemType File
        Add-Content -Path $PathToAssembly -Value "[assembly: AssemblyVersion(`"$BundleShortVersion`")]"
        Add-Content -Path $PathToAssembly -Value "[assembly: AssemblyFileVersion(`"$BundleShortVersion`")]"

    } else {

        $AbsolutePathToAssembly = Resolve-Path $PathToAssembly
        Get-ChildItem -Path $AbsolutePathToAssembly |
        ForEach-Object {
            $_.IsReadOnly = $false
            (Get-Content -Path $_) -replace '(?<=Assembly(?:File)?Version\(")[^"]*(?="\))', $BundleShortVersion | Set-Content -Path $_
        }

    }

    LogDebug "END updating AssemblyInfo"
}

#### MAIN SCRIPT #######################################

if ($Android) {
    SetAndroidVersion -PathToAndroidManifest $PathToAndroidManifest -VersionName $AppVersion -VersionCode $StringAppVersion
} elseif ($iOS) {
    SetIOSVersion -PathToPlist $PathToIOSInfoPlist -PathToAssembly $PathToIOSAssemblyInfo -BundleShortVersion $AppVersion -BundleVersion $StringAppVersion
} else {
    LogError "You must specify the platform to work with, setting a switch (e.g. <script-name> -Android <...>)"
    return
}
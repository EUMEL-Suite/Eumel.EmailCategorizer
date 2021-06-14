
$getVersionCommand = "gitversion.exe /Verbosity Quiet /showvariable MajorMinorPatch"
$gitMajorMinorPatch = (Invoke-Expression $getVersionCommand -ErrorAction Stop)
$buildId = $env:BUILD_BUILDID
if (!$buildId) { $buildId = "0" }
$fullVersion = $gitMajorMinorPatch + "." + $buildId


gitversion.exe /updateassemblyinfo | Out-Null

.\Set-ProjectFilesClickOnceVersion.ps1 -ProjectFilePath ".\Eumel.EmailCategorizer.Outlook\Eumel.EmailCategorizer.Outlook.csproj" -Version $fullVersion

$msbuild = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\msbuild.exe"

& $msbuild .\Eumel.EmailCategorizer.Outlook\Eumel.EmailCategorizer.Outlook.csproj /t:publish /p:configuration=release /v:q


$compress = @{
  Path = "..\publish\*"
  CompressionLevel = "Fastest"
  DestinationPath = "..\Eumel.EmailCategorizer.$($fullVersion).Zip"
}
Compress-Archive @compress -Force

# Magical Thing to Know

# Not sure if I need this
# Set-ItemProperty -Path 'HKCU:\SOFTWARE\EUMEL Suite\' -Name "Eumel.FirstStart" -Value 1
# Remove-ItemProperty -Path 'HKCU:\SOFTWARE\EUMEL Suite\' -Name "Eumel.FirstStart"


# This clears everything on startup
# Set-ItemProperty -Path 'HKCU:\SOFTWARE\EUMEL Suite\' -Name "Eumel.ClearOnStart" -Value 1

# Get-Item 'Registry::HKEY_CURRENT_USER\SOFTWARE\EUMEL Suite\' | Select-Object -ExpandProperty Property

#     Hive: HKEY_CURRENT_USER\SOFTWARE
# 
# Name                           Property
# ----                           --------
# EUMEL Suite                    Eumel.ConfigStore         : JsonFileEumelStorage
#                                Eumel.ConfigStoreSettings :
#                                Eumel.ClearOnStart        : 1

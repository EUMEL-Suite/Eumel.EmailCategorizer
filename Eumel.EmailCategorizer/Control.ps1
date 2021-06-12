
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
  Path = ".\Eumel.EmailCategorizer.Outlook\bin\Debug\app.publish\*"
  CompressionLevel = "Fastest"
  DestinationPath = "..\Eumel.EmailCategorizer.$($fullVersion).Zip"
}
Compress-Archive @compress -Force


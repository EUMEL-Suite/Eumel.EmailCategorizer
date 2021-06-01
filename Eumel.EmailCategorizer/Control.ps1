
$getVersionCommand = "gitversion.exe /Verbosity Quiet /showvariable MajorMinorPatch"
$gitMajorMinorPatch = (Invoke-Expression $getVersionCommand -ErrorAction Stop)
$buildId = $env:BUILD_BUILDID
if (!$buildId) { $buildId = "0" }
$fullVersion = $gitMajorMinorPatch + "." + $buildId


gitversion.exe /updateassemblyinfo | Out-Null

.\Set-ProjectFilesClickOnceVersion.ps1 -ProjectFilePath ".\Eumel.EmailCategorizer.Outlook\Eumel.EmailCategorizer.Outlook.csproj" -Version $fullVersion


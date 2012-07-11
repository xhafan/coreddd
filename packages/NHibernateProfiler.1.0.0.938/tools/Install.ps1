param($installPath, $toolsPath, $package, $project)

$profiler = "NHibernate"
$profilerExeName = "NHProf"

# Run the profiler
&"$toolsPath\$profilerExeName.exe"

# Determine if the target project is a Web Project.
$webConfig = $project.ProjectItems | Where-Object { $_.Name -eq "Web.config" }
if ($webConfig -eq $null) { # Not a web project
	# Remove the use of WebActivator
	$codePath = Join-Path (Split-Path $project.FullName -Parent) "App_Start\$($profiler)ProfilerBootstrapper.cs"
	(Get-Content $codePath) | Where-Object {$_ -notlike "*assembly: WebActivator.PreApplicationStartMethod*"} | Set-Content $codePath
	$codePath = Join-Path (Split-Path $project.FullName -Parent) "App_Start\$($profiler)ProfilerBootstrapper.vb"
	(Get-Content $codePath) | Where-Object {$_ -notlike "*assembly: WebActivator.PreApplicationStartMethod*"} | Set-Content $codePath
	
	$projectPath = $project.FullName
	$lang, $appStartFileName, $regexMainMethod, $preStartCall
	if ($projectPath.EndsWith("csproj")) {
		$lang = "cs"
		$appStartFileName = "Program.cs"
		$regexMainMethod = "static void Main.*\(.*\)[^{]+{"
		$preStartCall = "App_Start.$($profiler)ProfilerBootstrapper.PreStart();" + [System.Environment]::NewLine
	}
	elseif ($projectPath.EndsWith("vbproj")) {
		$lang = "vb"
		$appStartFileName = "Module1.vb"
		$regexMainMethod = "Sub Main.*\(.*\).*\n?"
		$preStartCall = "App_Start.$($profiler)ProfilerBootstrapper.PreStart()" + [System.Environment]::NewLine
	}
	else {
		return;
	}
	
	$appStartFilePath = Join-Path (Split-Path $projectPath -Parent) $appStartFileName
	if (Test-Path $appStartFilePath) {
		# Inject a call to PreStart inside the application start method
		# Get program.cs/Module1.vb file content
		$programFileContent = ""
		(Get-Content $appStartFilePath) | Foreach-Object { $programFileContent += $_ + [System.Environment]::NewLine }
		
		# Get the main method signature
		$match = [Regex]::Match($programFileContent, $regexMainMethod)
		$mainSignature = ""
		if ($match.Success)
		{
			$mainSignature = $match.Value
		}
		
		$programFileContent.Insert($programFileContent.IndexOf($mainSignature) + $mainSignature.Length, $preStartCall) | Set-Content $appStartFilePath
	}
}


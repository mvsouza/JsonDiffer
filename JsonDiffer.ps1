[CmdletBinding()]
param(
    [parameter(Mandatory=$true, Position=1)]
    [ValidateSet("openCover", "sonarqubeLocalBuild", "statusSonarqube", "startSonarqubeContainer","stopSonarqubeContainer", "installDependencies", "sonarCloudBuild")]
    [string]$action,
    [parameter(Mandatory=$false)]
    [switch]$all
)
process {
    #Requires -Modules Set-PsEnv
    Set-PsEnv

    function SonarqubeStatus($add){
        $status = Invoke-RestMethod "$add/api/system/status";
        return $status.status -eq "UP";
    }
    function RunOpenCover($projectName, $outputDir, $opencoverFile){
        $unitTestProj = Get-ChildItem "$PSScriptRoot\*$_*UnitTest.csproj" -Recurse;
        $dotnet = "C:\Program Files\dotnet\dotnet.exe";
        $opencoverFilter = "+[$projectName*]* -[*UnitTest]*";
        $target = "test --logger:trx;LogFileName=$outputDir\results.trx $($unitTestProj.FullName)";
        OpenCover.Console.exe -register:user -target:"$dotnet" -targetargs:"$target" -filter:"$opencoverFilter" -oldStyle -output:"$opencoverFile";

    }

    $tasks = @{};

    $tasks.Add("sonarCloudBuild",@{
        description="Runs build and Sonnar Scanner on SonarCloud.";
        script = {
            #Requires -Modules Set-PsEnv
            SonarQube.Scanner.MSBuild.exe begin /k:"$env:sonarcloud_key" /d:sonar.organization="$env:sonarcloud_org" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="$env:sonarcloud_login" /d:sonar.cs.opencover.reportsPaths="OpenCover.xml";
            dotnet msbuild;
            OpenCover.Console.exe -register:user -target:"C:\Program Files\dotnet\dotnet.exe" -targetargs:"test --logger:trx;LogFileName=results.trx /p:DebugType=full test\JsonDiffer.UnitTest\JsonDiffer.UnitTest.csproj" -filter:"+[JsonDiffer*]* -[*.Test*]*" -oldStyle -output:"OpenCover.xml";
            SonarQube.Scanner.MSBuild.exe end /d:sonar.login="$env:sonarcloud_login";
            codecov -f .\OpenCover.xml -t $env:codecov_token;
        }
    });
    $tasks.Add("startSonarqubeContainer",@{
        description="";
        script = {
            docker run -d -p 9000:9000 sonarqube;
        }
    });
    $tasks.Add("stopSonarqubeContainer",@{
        description="";
        script = {
            docker rm $(docker ps -f ancestor=sonarqube -a -q);
        }
    });
    $tasks.Add("installDependencies",@{
        description="";
        script = {
            if(Get-Command choco -errorAction SilentlyContinue){
                choco install -y reportgenerator.portable OpenCover msbuild-sonarqube-runner docker-for-windows;
            } 
            else{
                Write-Output "Install chocolatey first using: Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))";
            }
        }
    });
    $tasks.Add("statusSonarqube",@{
        description="";
        script = {
            Invoke-RestMethod "http://localhost:9000/api/system/status";
        }
    });
    $tasks.Add("sonarqubeLocalBuild",@{
        description="";
        script = {
            $projectName = "JsonDiffer";
            $sln = Get-ChildItem "$PSScriptRoot\$projectName.sln" -Recurse;
            $outputDir="$($sln.DirectoryName)\CodeCover";
            if(!(Test-Path $outputDir)){
                mkdir $outputDir;
            }
            $opencoverFile="$outputDir\OpenCover.xml";
            
            SonarQube.Scanner.MSBuild.exe begin /k:"$projectName" /v:"1.0" /n:"$projectName" /d:sonar.cs.opencover.reportsPaths="$opencoverFile";
            dotnet restore $sln.FullName;
            dotnet msbuild $sln.FullName;
            RunOpenCover $projectName $outputDir $opencoverFile;
            SonarQube.Scanner.MSBuild.exe end;
            [System.Diagnostics.Process]::Start("http://localhost:9000/dashboard?id=$projectName");
        }
    });
    $tasks.Add("openCover",@{
        description="";
        script = {
            $projectName = "JsonDiffer";
            $sln = Get-ChildItem "$PSScriptRoot\$projectName.sln" -Recurse;
            $outputDir="$($sln.DirectoryName)\CodeCover";
            if(!(Test-Path $outputDir)){
                mkdir $outputDir;
            }
            $opencoverFile="$outputDir\OpenCover.xml";
            RunOpenCover $projectName $outputDir $opencoverFile;
            reportgenerator -reports:"$opencoverFile" -targetdir:"$outputDir";
            & "$outputDir\Index.htm";
        }
    });
    $task = $tasks.Get_Item($action)
    if ($task) {
        Invoke-Command $task.script
    }
}

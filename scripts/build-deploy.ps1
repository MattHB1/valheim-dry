# Build Dry and copy the DLL into the r2modman profile plugins folder.
# Usage: .\scripts\build-deploy.ps1
# Optional: .\scripts\build-deploy.ps1 -Profile "Default"
# For Thunderstore / GitHub release zips, use: .\scripts\create-release.ps1

param(
  [string]$Profile = "Default"
)

$ErrorActionPreference = "Stop"

$RepoRoot = Split-Path $PSScriptRoot -Parent
$Project = Join-Path $RepoRoot "Dry\Dry.csproj"
$OutDll = Join-Path $RepoRoot "Dry\bin\Release\net4.8\Dry.dll"
$PluginDir = Join-Path $env:APPDATA "r2modmanPlus-local\Valheim\profiles\$Profile\BepInEx\plugins\MattHB1-Dry"

dotnet build $Project -c Release
if ($LASTEXITCODE -ne 0) { throw "Build failed" }

New-Item -ItemType Directory -Force -Path $PluginDir | Out-Null
Copy-Item $OutDll (Join-Path $PluginDir "Dry.dll") -Force
Write-Host "Deployed to $PluginDir\Dry.dll"
Write-Host "Launch Valheim via r2modman (profile: $Profile) to test."

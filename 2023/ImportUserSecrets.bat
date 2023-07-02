@ECHO OFF
TYPE Secrets.json | dotnet user-secrets set -p Core\Core.csproj --id 80e8ca33-2861-475d-8d94-983743fcc621
TYPE Secrets.json | dotnet user-secrets set -p Full\Full.csproj --id 80e8ca33-2861-475d-8d94-983743fcc621
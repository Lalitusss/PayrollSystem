$SolutionPath = "C:\Sources\PayrollSystem"
cd $SolutionPath

Write-Host "=== REORGANIZACION DE ARQUITECTURA ===" -ForegroundColor Cyan

# PASO 1: Services
Write-Host "1. Actualizando Services..." -ForegroundColor Yellow
$serviceFiles = Get-ChildItem -Path "$SolutionPath\Payroll.Services\Implementations" -Filter "*.cs"
foreach ($file in $serviceFiles) {
    $content = [System.IO.File]::ReadAllText($file.FullName)
    $content = $content -replace 'using Payroll\.Domain\.Entities;', 'using Payroll.Core.Entities;'
    [System.IO.File]::WriteAllText($file.FullName, $content)
    Write-Host "  OK: $($file.Name)" -ForegroundColor Green
}

# PASO 2: Data
Write-Host "2. Actualizando Data..." -ForegroundColor Yellow
$dataFiles = Get-ChildItem -Path "$SolutionPath\Payroll.Data" -Filter "*.cs" -Recurse
foreach ($file in $dataFiles) {
    $content = [System.IO.File]::ReadAllText($file.FullName)
    $content = $content -replace 'using Payroll\.Domain\.Entities;', 'using Payroll.Core.Entities;'
    [System.IO.File]::WriteAllText($file.FullName, $content)
    Write-Host "  OK: $($file.Name)" -ForegroundColor Green
}

# PASO 3: Controllers
Write-Host "3. Actualizando Controllers..." -ForegroundColor Yellow
$controllerFiles = Get-ChildItem -Path "$SolutionPath\Payroll.API\Controllers" -Filter "*.cs"
foreach ($file in $controllerFiles) {
    $content = [System.IO.File]::ReadAllText($file.FullName)
    $content = $content -replace 'using Payroll\.Domain\.Entities;', 'using Payroll.Core.Entities;'
    $content = $content -replace 'using Payroll\.Domain;', 'using Payroll.Core.DTOs;'
    [System.IO.File]::WriteAllText($file.FullName, $content)
    Write-Host "  OK: $($file.Name)" -ForegroundColor Green
}

# PASO 4: Interfaces
Write-Host "4. Actualizando Interfaces..." -ForegroundColor Yellow
$interfaceFiles = Get-ChildItem -Path "$SolutionPath\Payroll.Services\Interfaces" -Filter "*.cs" -ErrorAction SilentlyContinue
foreach ($file in $interfaceFiles) {
    $content = [System.IO.File]::ReadAllText($file.FullName)
    $content = $content -replace 'using Payroll\.Domain\.Entities;', 'using Payroll.Core.Entities;'
    [System.IO.File]::WriteAllText($file.FullName, $content)
    Write-Host "  OK: $($file.Name)" -ForegroundColor Green
}

# PASO 5: Remover DbContext expuesto
Write-Host "5. Removiendo DbContext expuesto..." -ForegroundColor Yellow
$serviceFiles = Get-ChildItem -Path "$SolutionPath\Payroll.Services\Implementations" -Filter "*.cs"
foreach ($file in $serviceFiles) {
    $content = [System.IO.File]::ReadAllText($file.FullName)
    $content = $content -replace 'public PayrollDbContext Context => _context;', ''
    [System.IO.File]::WriteAllText($file.FullName, $content)
}
Write-Host "  OK" -ForegroundColor Green

# PASO 6: Build
Write-Host "6. Build..." -ForegroundColor Yellow
dotnet build

Write-Host "COMPLETADO" -ForegroundColor Green
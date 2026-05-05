$path = "C:\Sources\PayrollSystem"
cd $path

Write-Host "Corrigiendo problemas criticos..." -ForegroundColor Cyan

# 1. Namespace VinculosConceptosController
$file1 = "$path\Payroll.API\Controllers\VinculosConceptosController.cs"
$content1 = [System.IO.File]::ReadAllText($file1)
$content1 = $content1 -replace 'namespace Payroll\.Web\.Api\.Controllers;', 'namespace Payroll.API.Controllers;'
[System.IO.File]::WriteAllText($file1, $content1)
Write-Host "1. Namespace corregido" -ForegroundColor Green

# 2. PayrollDbContext - remover duplicado
$file2 = "$path\Payroll.Data\PayrollDbContext.cs"
$content2 = [System.IO.File]::ReadAllText($file2)
$lines = $content2.Split([System.Environment]::NewLine)
$unique = $lines | Select-Object -Unique
$content2 = $unique -join [System.Environment]::NewLine
[System.IO.File]::WriteAllText($file2, $content2)
Write-Host "2. PayrollDbContext limpio" -ForegroundColor Green

# 3. VinculosConceptosController - remover duplicado
$file3 = "$path\Payroll.API\Controllers\VinculosConceptosController.cs"
$content3 = [System.IO.File]::ReadAllText($file3)
$lines = $content3.Split([System.Environment]::NewLine)
$unique = $lines | Select-Object -Unique
$content3 = $unique -join [System.Environment]::NewLine
[System.IO.File]::WriteAllText($file3, $content3)
Write-Host "3. VinculosConceptosController limpio" -ForegroundColor Green

# 4. Build
Write-Host "4. Build..." -ForegroundColor Yellow
dotnet build

Write-Host "Completado" -ForegroundColor Green
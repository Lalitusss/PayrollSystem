$path = "C:\Sources\PayrollSystem"
cd $path

Write-Host "Corrigiendo GenericController y duplicidades..." -ForegroundColor Cyan

# 1. CORREGIR GenericController.cs - Remover constructor duplicado y arreglar retornos
$gcFile = "$path\Payroll.API\Controllers\GenericController.cs"

Write-Host "1. Arreglando GenericController..." -ForegroundColor Yellow

$content = [System.IO.File]::ReadAllText($gcFile)

# Remover constructor duplicado (lineas 22-25)
$content = $content -replace 'protected GenericController\(IVinculoConceptoService service\)\s*{[\s\S]*?this\.service = service;\s*}', ''

# Remover campo privado sin usar
$content = $content -replace 'private IVinculoConceptoService service;', ''

# Cambiar Get() para retornar TDto
$content = $content -replace 'public virtual async Task<ActionResult<T>> Get\(int id\)', 'public virtual async Task<ActionResult<TDto>> Get(int id)'

$content = $content -replace 'return entity == null \? NotFound\(\) : Ok\(entity\);', 'if (entity == null) return NotFound(); return Ok(entity.Adapt<TDto>());'

# Cambiar Post() para retornar TDto
$content = $content -replace 'public virtual async Task<ActionResult<T>> Post\(T entity\)', 'public virtual async Task<ActionResult<TDto>> Post(TDto dto)'

$content = $content -replace 'var created = await _service\.CreateAsync\(entity\);', 'var entity = dto.Adapt<T>(); var created = await _service.CreateAsync(entity);'

$content = $content -replace 'return CreatedAtAction\(nameof\(Get\), new \{ id = created\.Id \}, created\);', 'return CreatedAtAction(nameof(Get), new { id = created.Id }, created.Adapt<TDto>());'

[System.IO.File]::WriteAllText($gcFile, $content)
Write-Host "   OK: GenericController corregido" -ForegroundColor Green

# 2. Remover imports duplicados en PayrollDbContext
$dbFile = "$path\Payroll.Data\PayrollDbContext.cs"
Write-Host "2. Limpiando PayrollDbContext..." -ForegroundColor Yellow

$content = [System.IO.File]::ReadAllText($dbFile)
$lines = $content -split [System.Environment]::NewLine
$unique = @()
$seen = @{}

foreach ($line in $lines) {
    $trimmed = $line.Trim()
    if ($trimmed.StartsWith('using') -and $seen.ContainsKey($trimmed)) {
        continue
    }
    if ($trimmed.StartsWith('using')) {
        $seen[$trimmed] = $true
    }
    $unique += $line
}

[System.IO.File]::WriteAllText($dbFile, ($unique -join [System.Environment]::NewLine))
Write-Host "   OK: Imports duplicados removidos" -ForegroundColor Green

# 3. Remover imports duplicados en VinculosConceptosController
$vcFile = "$path\Payroll.API\Controllers\VinculosConceptosController.cs"
Write-Host "3. Limpiando VinculosConceptosController..." -ForegroundColor Yellow

$content = [System.IO.File]::ReadAllText($vcFile)
$lines = $content -split [System.Environment]::NewLine
$unique = @()
$seen = @{}

foreach ($line in $lines) {
    $trimmed = $line.Trim()
    if ($trimmed.StartsWith('using') -and $seen.ContainsKey($trimmed)) {
        continue
    }
    if ($trimmed.StartsWith('using')) {
        $seen[$trimmed] = $true
    }
    $unique += $line
}

[System.IO.File]::WriteAllText($vcFile, ($unique -join [System.Environment]::NewLine))
Write-Host "   OK: Imports duplicados removidos" -ForegroundColor Green

# 4. Build
Write-Host "4. Compilando..." -ForegroundColor Yellow
$build = dotnet build 2>&1

if ($LASTEXITCODE -eq 0) {
    Write-Host "   OK: Build exitoso" -ForegroundColor Green
} else {
    Write-Host "   ERROR: Hay errores de compilacion" -ForegroundColor Red
    Write-Host $build
}

Write-Host "`nCompletado!" -ForegroundColor Green
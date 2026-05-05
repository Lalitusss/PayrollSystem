$path = "C:\Sources\PayrollSystem"
cd $path

Write-Host "Unificando GenericService y eliminando personalizaciones..." -ForegroundColor Cyan

# 1. ACTUALIZAR GenericService.cs
Write-Host "1. Optimizando GenericService.cs..." -ForegroundColor Yellow

$gsFile = "$path\Payroll.Services\Implementations\GenericService.cs"
$gsContent = @"
using Microsoft.EntityFrameworkCore;
using Payroll.Core.Interfaces;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class GenericService<T> : IGenericService<T> where T : class, IEntity
{
    protected readonly PayrollDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericService(PayrollDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    protected virtual IQueryable<T> Query()
    {
        return _dbSet.AsNoTracking();
    }

    public virtual IQueryable<T> GetQueryable() => Query();

    public virtual async Task<IEnumerable<T>> GetAllAsync()
        => await Query().ToListAsync();

    public virtual async Task<T?> GetByIdAsync(int id)
        => await Query().FirstOrDefaultAsync(e => e.Id == id);

    public virtual async Task<T> CreateAsync(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
"@

[System.IO.File]::WriteAllText($gsFile, $gsContent)
Write-Host "   OK: GenericService optimizado" -ForegroundColor Green

# 2. SIMPLIFICAR CargoService
Write-Host "2. Simplificando CargoService..." -ForegroundColor Yellow

$csFile = "$path\Payroll.Services\Implementations\CargoService.cs"
$csContent = @"
using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class CargoService : GenericService<Cargo>, ICargoService
{
    public CargoService(PayrollDbContext context) : base(context)
    {
    }
}
"@

[System.IO.File]::WriteAllText($csFile, $csContent)
Write-Host "   OK: CargoService simplificado" -ForegroundColor Green

# 3. SIMPLIFICAR ConvenioService
Write-Host "3. Simplificando ConvenioService..." -ForegroundColor Yellow

$cvFile = "$path\Payroll.Services\Implementations\ConvenioService.cs"
$cvContent = @"
using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class ConvenioService : GenericService<Convenio>, IConvenioService
{
    public ConvenioService(PayrollDbContext context) : base(context)
    {
    }
}
"@

[System.IO.File]::WriteAllText($cvFile, $cvContent)
Write-Host "   OK: ConvenioService simplificado" -ForegroundColor Green

# 4. SIMPLIFICAR AsignacionCargoService
Write-Host "4. Simplificando AsignacionCargoService..." -ForegroundColor Yellow

$acFile = "$path\Payroll.Services\Implementations\AsignacionCargoService.cs"
$acContent = @"
using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class AsignacionCargoService : GenericService<AsignacionCargo>, IAsignacionCargoService
{
    public AsignacionCargoService(PayrollDbContext context) : base(context)
    {
    }
}
"@

[System.IO.File]::WriteAllText($acFile, $acContent)
Write-Host "   OK: AsignacionCargoService simplificado" -ForegroundColor Green

# 5. SIMPLIFICAR VinculoConceptoService
Write-Host "5. Simplificando VinculoConceptoService..." -ForegroundColor Yellow

$vcFile = "$path\Payroll.Services\Implementations\VinculoConceptoService.cs"
$vcContent = @"
using Payroll.Core.Entities;
using Payroll.Data;
using Payroll.Services.Interfaces;

namespace Payroll.Services.Implementations;

public class VinculoConceptoService : GenericService<VinculoConcepto>, IVinculoConceptoService
{
    public VinculoConceptoService(PayrollDbContext context) : base(context)
    {
    }
}
"@

[System.IO.File]::WriteAllText($vcFile, $vcContent)
Write-Host "   OK: VinculoConceptoService simplificado" -ForegroundColor Green

# 6. Build
Write-Host "6. Compilando..." -ForegroundColor Yellow
$build = dotnet build 2>&1

if ($LASTEXITCODE -eq 0) {
    Write-Host "   OK: Build exitoso" -ForegroundColor Green
} else {
    Write-Host "   ERROR: Revisa los errores" -ForegroundColor Red
    Write-Host $build
}

Write-Host "`nLimpieza completada!" -ForegroundColor Green
Write-Host "Ahora podes re-armar la logica personalizada cuando necesites." -ForegroundColor Cyan
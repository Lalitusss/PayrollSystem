using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Services.Interfaces;

public class GenericService<T> : IGenericService<T> where T : class
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
    public IQueryable<T> GetQueryable() => Query();

    public virtual async Task<IEnumerable<T>> GetAllAsync()
        => await Query()
            .AsNoTracking()
            .ToListAsync();

    public virtual async Task<T?> GetByIdAsync(int id)
        => await Query()
            .FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

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

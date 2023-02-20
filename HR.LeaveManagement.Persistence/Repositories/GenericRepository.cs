using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain.Common;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected HrDatabaseContext DbContext { get; }
    protected DbSet<T> DbSet { get; }
    protected IQueryable<T> Query => DbSet.AsNoTracking();

    public GenericRepository(HrDatabaseContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<T>();
    }

    public async Task<List<T>> GetAsync()
    {
        var allItems =  await Query.ToListAsync();
       
        return allItems;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var item = await Query.FirstOrDefaultAsync(el => el.Id == id);
        return item;
    }

    public async Task<T> CreateAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        await DbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        DbSet.Update(entity);
        await DbContext.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        DbSet.Remove(entity);
        await DbContext.SaveChangesAsync();

    }
}

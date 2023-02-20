using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDatabaseContext dbContext) : base(dbContext)
    {
    }

    public async Task AddAllocation(List<LeaveAllocation> allocation)
    {
       await DbSet.AddRangeAsync(allocation);
        await DbContext.SaveChangesAsync();
    }

    public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
    {
        var exists = await Query.AnyAsync(el => el.EmployeeId == userId && el.Period == period && el.LeaveTypeId == leaveTypeId);
        return exists;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        return await Query.Include(el => el.LeaveType).ToListAsync();
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
    {
        return await Query.Where(el => el.EmployeeId == userId)
            .Include(el => el.LeaveType).ToListAsync();
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        return await Query.Include(el => el.LeaveType).FirstOrDefaultAsync(el => el.Id == id);
    }

    public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
    {
        return await Query.Include(el => el.LeaveType)
            .FirstOrDefaultAsync(el => el.EmployeeId == userId && el.LeaveTypeId== leaveTypeId);
    }
}
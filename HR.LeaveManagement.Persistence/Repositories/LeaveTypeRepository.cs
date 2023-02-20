using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    public LeaveTypeRepository(HrDatabaseContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsLeaveTypeNameUnique(string name)
    {
        var nameExist = await Query.AnyAsync(el => el.Name == name);
        return !nameExist;

    }
}


using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(HrDatabaseContext dbContext) : base(dbContext)
    {
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
    {
        var leaveRequest = await Query.Include(q => q.LeaveType)
            .FirstOrDefaultAsync(q => q.Id == id);
        return leaveRequest;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestWithDetails()
    {
        var leaveRequests = await Query.Include(q => q.LeaveType)
            .ToListAsync();
        return leaveRequests;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestWithDetails(string userId)
    {
        var leaveRequests = await Query.Where(q => q.RequestingEmployeeId == userId)
            .Include(q => q.LeaveType)
            .ToListAsync();
        return leaveRequests;
    }
}


namespace HR.LeaveManagement.Application.Features.LeaveType.Commands;

public class NotFoundException:Exception
{
  public NotFoundException(string name, object key) : base($"{name} ({key}) was not found.") { }
}



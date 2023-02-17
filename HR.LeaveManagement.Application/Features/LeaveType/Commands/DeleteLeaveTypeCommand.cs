using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands;

public class DeleteLeaveTypeCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}


public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
    }
    private ILeaveTypeRepository _leaveTypeRepository { get; }

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // validation

        var domainEntity = await _leaveTypeRepository.GetByIdAsync(request.Id);

        if (domainEntity == null)
        { 
            throw new NotFoundException(nameof(Domain.LeaveType), request.Id);
        }


        await _leaveTypeRepository.DeleteAsync(domainEntity);

        return Unit.Value;
    }
}
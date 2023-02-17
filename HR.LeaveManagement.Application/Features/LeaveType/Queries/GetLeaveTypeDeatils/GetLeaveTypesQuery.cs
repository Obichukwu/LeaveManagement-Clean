using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public record GetLeaveTypeDetailQuery(int Id) : IRequest<LeaveTypeDetailDto>;


public class GetLeaveTypeDetailQueryHandler : IRequestHandler<GetLeaveTypeDetailQuery, LeaveTypeDetailDto>
{
    public GetLeaveTypeDetailQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    private IMapper _mapper { get; }
    private  ILeaveTypeRepository _leaveTypeRepository { get; }

    public async Task<LeaveTypeDetailDto> Handle(GetLeaveTypeDetailQuery request, CancellationToken cancellationToken)
    {
        var leaveType = await  _leaveTypeRepository.GetByIdAsync(request.Id);

        if (leaveType == null)       
            throw new NotFoundException(nameof(Domain.LeaveType), request.Id);

        var dtos = _mapper.Map<LeaveTypeDetailDto>(leaveType);
        return dtos;
    }
}

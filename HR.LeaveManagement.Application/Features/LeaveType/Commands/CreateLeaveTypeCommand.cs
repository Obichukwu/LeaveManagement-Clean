using AutoMapper;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands;

public class CreateLeaveTypeCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    private ILeaveTypeRepository _leaveTypeRepository { get; }

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

        RuleFor(p => p.DefaultDays)
            .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
            .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");

        RuleFor(p => p)
            .MustAsync(LeaveTypeNameUnique).WithMessage("Leavetype already exist");
    }

    private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand createLeaveTypeCommand, CancellationToken cancellationToken)
    {
        return _leaveTypeRepository.IsLeaveTypeNameUnique(createLeaveTypeCommand.Name);
    }
}

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    private IMapper _mapper { get; }
    private ILeaveTypeRepository _leaveTypeRepository { get; }

    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (validatorResult.Errors.Any()) {
            throw new BadRequestException("Invalid leavetype", validatorResult);
        }

        var domainEntity = _mapper.Map<Domain.LeaveType>(request);

        await _leaveTypeRepository.CreateAsync(domainEntity);

        return domainEntity.Id;
    }
}

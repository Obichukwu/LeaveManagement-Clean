﻿using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveType.Commands;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDeatils;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.MappingProfile;

public class LeaveTypeProfile: Profile
{
    public LeaveTypeProfile() { 
        CreateMap<LeaveType,LeaveTypeDto>().ReverseMap();
        CreateMap<LeaveType,LeaveTypeDetailDto>().ReverseMap();
        CreateMap<CreateLeaveTypeCommand, LeaveType>();
    }
}

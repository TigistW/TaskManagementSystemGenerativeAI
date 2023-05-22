using System.Runtime.Intrinsics.X86;
using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.Users.CQRS.Queries;
using TaskManagementSystem.Application.Features.Users.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.Users.CQRS.Handlers;

public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, BaseCommandResponse<GetUserByIdDto>>
{

    private readonly IUnitOfWorks _unitOfWorks;

    private readonly IMapper _mapper;

    public GetUserDetailQueryHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
    {
        _mapper =  mapper;
        _unitOfWorks = unitOfWorks;
    }

    public async Task<BaseCommandResponse<GetUserByIdDto>> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {

        var user = await _unitOfWorks.UserRepository.Get(request.Id);
        var userDetail = _mapper.Map<GetUserByIdDto>(user);

        var result = new BaseCommandResponse<GetUserByIdDto>();
        result.Value = userDetail;
        return result;
    }
}

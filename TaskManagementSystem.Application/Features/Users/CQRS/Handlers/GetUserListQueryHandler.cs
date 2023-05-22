using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.Users.CQRS.Queries;
using TaskManagementSystem.Application.Features.Users.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.Users.CQRS.Handlers;

public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, BaseCommandResponse<List<GetUserListDto>>>
{
    private readonly IUnitOfWorks _unitOfWorks;

    private readonly IMapper _mapper;

    public GetUserListQueryHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWorks = unitOfWorks;
    }
    public async Task<BaseCommandResponse<List<GetUserListDto>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {

        var users = await _unitOfWorks.UserRepository.GetAll();
        var usersList = _mapper.Map<List<GetUserListDto>>(users);
        var result = new BaseCommandResponse<List<GetUserListDto>>();
        result.Value = usersList;
        return result;
        
    }
}

using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.UserChore.CQRS.Queries;
using TaskManagementSystem.Application.Features.UserChore.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.UserChore.CQRS.Handlers;

public class GetChoreListQueryHandler : IRequestHandler<GetChoreListQueries, BaseCommandResponse<List<ChoreListDto>>>
{
    private readonly IUnitOfWorks _unitOfWork;
    private readonly IMapper _mapper;

    public GetChoreListQueryHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
    {
        _unitOfWork = unitOfWorks;
        _mapper = mapper;
    }
    public async Task<BaseCommandResponse<List<ChoreListDto>>> Handle(GetChoreListQueries request, CancellationToken cancellationToken)
    {
        var tasks = await _unitOfWork.ChoreRepository.GetAll();
        var taskList = _mapper.Map<List<ChoreListDto>>(tasks);

        var result = new BaseCommandResponse<List<ChoreListDto>>();
        result.Value = taskList;
        result.Message = "Fetch Successful";

        return result;
    }
}

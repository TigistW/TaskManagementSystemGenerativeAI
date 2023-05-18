using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.UserChore.CQRS.Queries;
using TaskManagementSystem.Application.Features.UserChore.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.UserChore.CQRS.Handlers;



public class GetChoreDetailQueryHandler : IRequestHandler<GetChoreDetailQueries, BaseCommandResponse<ChoreDetailDto>>
{
    private readonly IUnitOfWorks _unitOfWork;
    private readonly IMapper _mapper;

    public GetChoreDetailQueryHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
    {
        _unitOfWork = unitOfWorks;
        _mapper = mapper;
    }
    public async Task<BaseCommandResponse<ChoreDetailDto>> Handle(GetChoreDetailQueries request, CancellationToken cancellationToken)
    {
        var tasks = await _unitOfWork.ChoreRepository.Get(request.Id);
        var taskList = _mapper.Map<ChoreDetailDto>(tasks);

        var result = new BaseCommandResponse<ChoreDetailDto>();
        result.Value = taskList;
        result.Message = "Fetch Successful";

        return result;
    }
}

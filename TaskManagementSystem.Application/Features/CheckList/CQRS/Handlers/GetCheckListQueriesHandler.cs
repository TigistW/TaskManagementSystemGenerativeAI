using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Queries;
using TaskManagementSystem.Application.Features.CheckList.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Handlers;

public class GetCheckListQueriesHandler : IRequestHandler<GetCheckListQueries, BaseCommandResponse<List<GetCheckListDto>>>
{
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMapper _mapper;
    public GetCheckListQueriesHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWorks = unitOfWorks;
        
    }
    public async Task<BaseCommandResponse<List<GetCheckListDto>>> Handle(GetCheckListQueries request, CancellationToken cancellationToken)
    {
        var result = new BaseCommandResponse<List<GetCheckListDto>>();
        var checkList = await _unitOfWorks.CheckListRepository.GetAll();
        var mappedCheckList = _mapper.Map<List<GetCheckListDto>>(checkList);
        result.Value = mappedCheckList;
        return result;
    }
}

using AutoMapper;
using MediatR;
using TaskManagementSystem.Domain;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Commands;
using TaskManagementSystem.Application.Features.CheckList.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Handlers;

public class CreateCheckListCommandHandler:  IRequestHandler <CreateCheckListCommand, BaseCommandResponse<CreateCheckListDto>>
{
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMapper _mapper;
    public CreateCheckListCommandHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWorks = unitOfWorks;

    }
    public async Task<BaseCommandResponse<CreateCheckListDto>> Handle(CreateCheckListCommand request, CancellationToken cancellationToken)
    {
        var result = new BaseCommandResponse<CreateCheckListDto>();
        var checkList = _mapper.Map<Domain.CheckList>(request.createCheckListDto);
        var createdList = await _unitOfWorks.CheckListRepository.Add(checkList);
        await _unitOfWorks.Save();
        result.Value = _mapper.Map<CreateCheckListDto>(createdList);
        return result;
    }
}
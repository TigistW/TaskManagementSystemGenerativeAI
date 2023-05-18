using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Commands;
using TaskManagementSystem.Application.Features.CheckList.DTO;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Handlers;

public class UpdateCheckListCommandHandler : IRequestHandler<UpdateCheckListCommand, BaseCommandResponse<UpdateCheckListDto>>
{

    private readonly IUnitOfWorks _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCheckListCommandHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
    {
        _unitOfWork = unitOfWorks;
        _mapper = mapper;
    }
    public async Task<BaseCommandResponse<UpdateCheckListDto>> Handle(UpdateCheckListCommand request, CancellationToken cancellationToken)
    {
        var checklist = _mapper.Map<Domain.CheckList>(request.updateCheckListDto);
        await _unitOfWork.CheckListRepository.Update(checklist);
        await _unitOfWork.Save();
        var result = new BaseCommandResponse<UpdateCheckListDto>();
        result.Value = _mapper.Map<UpdateCheckListDto>(checklist);
        result.Message = "Update Successful";
        return result;
    }
}

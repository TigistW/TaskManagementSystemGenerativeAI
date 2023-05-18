using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.UserChore.CQRS.Commands;
using TaskManagementSystem.Application.Features.UserChore.DTO;
using TaskManagementSystem.Application.Responses;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.UserChore.CQRS.Handlers;

public class UpdateChoreCommandHandler : IRequestHandler<UpdateChoreCommand, BaseCommandResponse<UpdateChoreDto>>
{
    private readonly IUnitOfWorks _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateChoreCommandHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
    {
        _unitOfWork = unitOfWorks;
        _mapper = mapper;
    }
    public async Task<BaseCommandResponse<UpdateChoreDto>> Handle(UpdateChoreCommand request, CancellationToken cancellationToken)
    {
        var chore = _mapper.Map<Chore>(request.updateChoreDto);
        await _unitOfWork.ChoreRepository.Update(chore);
        await _unitOfWork.Save();
        var result = new BaseCommandResponse<UpdateChoreDto>();
        result.Value = _mapper.Map<UpdateChoreDto>(chore);
        result.Message = "Update Successful";
        return result;
    }
}

using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.UserChore.CQRS.Commands;
using TaskManagementSystem.Application.Responses;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.UserChore.CQRS.Handlers;

public class DeleteChoreCommandHandler: IRequestHandler<DeleteChoreCommand, BaseCommandResponse<Unit>>
{
private readonly IUnitOfWorks _unitOfWork;
private readonly IMapper _mapper;

public DeleteChoreCommandHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
{
    _unitOfWork = unitOfWorks;
    _mapper = mapper;
}

public async Task<BaseCommandResponse<Unit>>Handle(DeleteChoreCommand request, CancellationToken cancellationToken)
{
    var chore = _mapper.Map<Chore>(request.deleteChoreDto);
    await _unitOfWork.ChoreRepository.Delete(chore);
        await _unitOfWork.Save();
    return new BaseCommandResponse<Unit>();
}
}

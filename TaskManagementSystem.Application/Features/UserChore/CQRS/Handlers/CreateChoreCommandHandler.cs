using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.UserChore.CQRS.Commands;
using TaskManagementSystem.Application.Features.UserChore.DTO;
using TaskManagementSystem.Application.Responses;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.UserChore.CQRS.Handlers;

public class CreateChoreCommandHandler : IRequestHandler<CreateChoreCommand, BaseCommandResponse<CreateChoreDto>>
{
    private readonly IUnitOfWorks _unitOfWork;
    private readonly IMapper _mapper;

    public CreateChoreCommandHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
    {
        _unitOfWork = unitOfWorks;
        _mapper = mapper;
    }
    public async Task<BaseCommandResponse<CreateChoreDto>> Handle(CreateChoreCommand request, CancellationToken cancellationToken)
    {
        var chore = _mapper.Map<Chore>(request.createChoreDto);
        chore = await _unitOfWork.ChoreRepository.Add(chore);
        await _unitOfWork.Save();
        var result = new BaseCommandResponse<CreateChoreDto>();
        result.Value = _mapper.Map<CreateChoreDto>(chore);
        result.Message = "Fetch Successful";
        return result;
    }
}

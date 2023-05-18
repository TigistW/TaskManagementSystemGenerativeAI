using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Commands;
using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Handlers;

public class DeleteCheckListCommandHandler : IRequestHandler<DeleteCheckListCommand, BaseCommandResponse<Unit>>
{
    private readonly IUnitOfWorks _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteCheckListCommandHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
    {
        _unitOfWork = unitOfWorks;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse<Unit>> Handle(DeleteCheckListCommand request, CancellationToken cancellationToken)
    {
        var checklist = _mapper.Map<Domain.CheckList>(request.deleteCheckListDto);
        await _unitOfWork.CheckListRepository.Delete(checklist);
        await _unitOfWork.Save();
        return new BaseCommandResponse<Unit>();
    }
}

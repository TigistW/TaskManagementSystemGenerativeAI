using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.Users.CQRS.Commands;
using TaskManagementSystem.Application.Responses;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.Users.CQRS.Handlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, BaseCommandResponse<Unit>>
{
    private readonly IUnitOfWorks _unitOfWorks;

    private readonly IMapper _mapper;

    public DeleteUserCommandHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWorks = unitOfWorks;

    }
    
    public async Task<BaseCommandResponse<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        
        var user = await _unitOfWorks.UserRepository.Get(request.deleteUserDto.Id);
        _mapper.Map<User>(user);
        await _unitOfWorks.UserRepository.Delete(user);
        await _unitOfWorks.Save();
        return new BaseCommandResponse<Unit>();
    }
}

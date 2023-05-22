using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.Users.CQRS.Commands;
using TaskManagementSystem.Application.Features.Users.DTO;
using TaskManagementSystem.Application.Responses;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.Users.CQRS.Handlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseCommandResponse<UpdateUserDto>>
{
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWorks = unitOfWorks;
    }
    
    public async Task<BaseCommandResponse<UpdateUserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.updateUserDto);
        await _unitOfWorks.UserRepository.Update(user);
        await _unitOfWorks.Save();
        var result = new BaseCommandResponse<UpdateUserDto>();
        result.Value = _mapper.Map<UpdateUserDto>(user);
        return result;
    }
}

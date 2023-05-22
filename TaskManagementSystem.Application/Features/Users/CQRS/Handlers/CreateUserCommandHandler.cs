using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.Users.CQRS.Commands;
using TaskManagementSystem.Application.Features.Users.DTO;
using TaskManagementSystem.Application.Responses;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Features.Users.CQRS.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, BaseCommandResponse<CreateUserDto>>
{
    private readonly IUnitOfWorks _unitOfWorks;

    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWorks = unitOfWorks;

    }

    public async Task<BaseCommandResponse<CreateUserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.createUserDto);
        var createdUser = await _unitOfWorks.UserRepository.Add(user);
        await _unitOfWorks.Save();

        var result = new BaseCommandResponse<CreateUserDto>();
        result.Value = _mapper.Map<CreateUserDto>(createdUser);

        return result;
    }
}

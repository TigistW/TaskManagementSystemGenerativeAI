using AutoMapper;
using TaskManagementSystem.Application.Features.UserChore.DTO;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Mappings;

public class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<CreateChoreDto, Chore>().ReverseMap();
        CreateMap<ChoreListDto, Chore>().ReverseMap();
        CreateMap<ChoreDetailDto, Chore>().ReverseMap();
        CreateMap<DeleteChoreDto, Chore>().ReverseMap();
        CreateMap<UpdateChoreDto, Chore>().ReverseMap();

    }
}

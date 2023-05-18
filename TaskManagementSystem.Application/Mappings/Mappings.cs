using AutoMapper;
using TaskManagementSystem.Application.Features.CheckList.DTO;
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

        CreateMap<CreateCheckListDto, CheckList>().ReverseMap();
        CreateMap<GetCheckListDto, CheckList>().ReverseMap();
        CreateMap<DeleteCheckListDto, CheckList>().ReverseMap();
        CreateMap<UpdateCheckListDto, CheckList>().ReverseMap();

    }
}

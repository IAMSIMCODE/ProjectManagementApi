using AutoMapper;
using Entities.DbSet;
using Entities.Dtos.Response;

namespace ProjManagement.Api.MappingProfile
{
    public class DomainToResponse : Profile
    {
        public DomainToResponse()
        {
            CreateMap<Achievement, DeveloperAchievementResponse>();


            CreateMap<Developer, GetDeveloperResponse>()
                .ForMember(
                    dest => dest.DeveloperId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
                );
        }
    }
}

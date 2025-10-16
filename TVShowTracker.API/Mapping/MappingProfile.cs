using AutoMapper;
using TVShowTracker.API.DTOs;
using TVShowTracker.API.Models;

namespace TVShowTracker.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Map TVShow entity to TVShowListDto
            CreateMap<TVShow, TVShowListDto>();

            //Map TVShow entity to TVShowDetailDto
            CreateMap<TVShow, TVShowDetailDto>();

            //Map Episode entity to EpisodeDto
            CreateMap<Episode, EpisodeDto>();

            //Map Actor entity to ActorDto
            CreateMap<Actor, ActorDto>().ForMember(dest => dest.TVShows, opt => opt.MapFrom(src => src.ShowActors.Select(sa => sa.TVShow)));


        }
    }
}

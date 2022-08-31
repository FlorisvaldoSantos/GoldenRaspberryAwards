using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DTOMapperProfile : Profile
    {
        public DTOMapperProfile()
        {
            CreateMap<ProducerDTO, ProducerDTO>()
                 .ForMember(dst => dst.Producer, map => map.MapFrom(src => src.Producer))
                 .ForMember(dst => dst.PreviousWin, map => map.MapFrom(src => src.PreviousWin))
                 .ForMember(dst => dst.FollowingWin, map => map.MapFrom(src => src.FollowingWin))
                 .ForMember(dst => dst.Interval, map => map.MapFrom(src => src.Interval));

            CreateMap<ProducerDTO, ProducerModel>()
                 .ForMember(dst => dst.Id, map => map.MapFrom(src => src.Id))
                 .ForMember(dst => dst.Producer, map => map.MapFrom(src => src.Producer))
                 .ForMember(dst => dst.PreviousWin, map => map.MapFrom(src => src.PreviousWin))
                 .ForMember(dst => dst.FollowingWin, map => map.MapFrom(src => src.FollowingWin))
                 .ForMember(dst => dst.Interval, map => map.MapFrom(src => src.Interval));

            CreateMap<ProducerModel, ProducerDTO>()
                 .ForMember(dst => dst.Id, map => map.MapFrom(src => src.Id))
                 .ForMember(dst => dst.Producer, map => map.MapFrom(src => src.Producer))
                 .ForMember(dst => dst.PreviousWin, map => map.MapFrom(src => src.PreviousWin))
                 .ForMember(dst => dst.FollowingWin, map => map.MapFrom(src => src.FollowingWin))
                 .ForMember(dst => dst.Interval, map => map.MapFrom(src => src.Interval));
           

        }
    }
}

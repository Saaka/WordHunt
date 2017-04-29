using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WordHunt.Data.Entities;
using WordHunt.Models.Games.Creation;

namespace WordHunt.Mappings
{
    public class WordHuntMapperProfile : Profile
    {
        public WordHuntMapperProfile()
        {
            CreateMap<GameCreate, Game>();
            CreateMap<GameTeamCreate, GameTeam>()
                .ForMember(d=> d.RemainingFieldCount, opt => opt.MapFrom(s => s.FieldCount));
        }
    }
}

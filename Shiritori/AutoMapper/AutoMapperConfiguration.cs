using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Library.EFModels;
using Library.ViewModels;

namespace Shirirtoi.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void ConfigMaps()
        {
            Mapper.Initialize(c => {
                c.CreateMap<Game, GameVM>().ReverseMap();
                c.CreateMap<GameVM, PlayGameVM>().ReverseMap();
                c.CreateMap<UserProfile, UserProfileVM>().ReverseMap();
                c.CreateMap<WordBank, WordBankVM>().ReverseMap();
                c.CreateMap<Role, RoleVM>().ReverseMap();
            });
        }
    }
}
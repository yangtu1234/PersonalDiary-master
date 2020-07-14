using AutoMapper;
using PersonalDiary.Dtos;
using PersonalDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalDiary.Profiles
{
    public class UsersProfiles: Profile
    {
        public UsersProfiles()
        {
            CreateMap<Users, UsersDto>();
            CreateMap<UserAddDto, Users>();
            CreateMap<UsersEditDto, Users>();
        }
    }
}

using AutoMapper;
using PersonalDiary.Dtos;
using PersonalDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalDiary.Profiles
{
    public class DiaryProfiles: Profile
    {
        public DiaryProfiles()
        {
            CreateMap<Diary, DiaryDto>();
            CreateMap<DiaryAddDto, Diary>();
            CreateMap<DiaryEditDto, Diary>();
        }
    }
}

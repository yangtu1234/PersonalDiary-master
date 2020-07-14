using PersonalDiary.Models;
using PersonalDiary.Repository.IRepository;
using PersonalDiary.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalDiary.Services.Services
{
    public class DiaryService:BaseService<Diary>, IDiaryService
    {
        public DiaryService(IDiaryRepository diaryRepository)
        {
            base.CurrentRepository = diaryRepository;
        }
    }
}

using PersonalDiary.Models;
using PersonalDiary.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalDiary.Repository.Repository
{
    public class UsersRepository : BaseRepository<Users>, IUsersRepository
    {
        public UsersRepository(PersonalDiaryContext personalDiaryContext) : base(personalDiaryContext)
        {
        }
    }
}

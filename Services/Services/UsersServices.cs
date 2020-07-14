using Microsoft.EntityFrameworkCore;
using PersonalDiary.Dtos;
using PersonalDiary.Models;
using PersonalDiary.Repository.IRepository;
using PersonalDiary.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalDiary.Services.Services
{
    public class UsersServices : BaseService<Users>, IUsersService
    {
        private readonly IUsersRepository usersRepository;

        public UsersServices(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
            base.CurrentRepository = usersRepository;
        }
        public async Task<Users> Login(LoginDto loginDto)
        {
            return await usersRepository.GetEntitys(u => u.UsersPhone == loginDto.UsersPhone && u.UsersPwd == loginDto.UsersPwd).FirstOrDefaultAsync();
        }
    }
}

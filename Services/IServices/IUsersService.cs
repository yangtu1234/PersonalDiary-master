using PersonalDiary.Dtos;
using PersonalDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalDiary.Services.IServices
{
    public interface IUsersService:IBaseService<Users>
    {
        Task<Users> Login(LoginDto loginDto);
    }
}

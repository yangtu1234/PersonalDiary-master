using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalDiary.Dtos;
using PersonalDiary.Models;
using PersonalDiary.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalDiary.Controllers
{
    [ApiController]
    [Route("api/Users")]
    [Authorize]
    public class UsersControllers:ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public UsersControllers(IUsersService usersService,IMapper mapper)
        {
            this.usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDto>>> GetEntitys(int pageIndex, int pageSize, bool isAsc)
        {
            int totalCount = 0;
            var users =await usersService.GetEntityForPage(pageIndex, pageSize,out totalCount,u=>true,u=>u.UsersId, isAsc).ToArrayAsync();
            if (users.Count() == 0)
            {
                return NotFound();
            }
            var ToUserDto = mapper.Map<IEnumerable<UsersDto>>(users);
            return Ok(ToUserDto);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UsersDto>> GetEntitysById(int? id)
        {
            var user = await usersService.GetEntitys(u => u.UsersId == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var ToUserDto = mapper.Map<UsersDto>(user);
                return Ok(ToUserDto);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<MessageInfo<UsersDto>>> AddEntity(UserAddDto userAddDto)
        {
            MessageInfo<UsersDto> msge = new MessageInfo<UsersDto>();
            if (string.IsNullOrWhiteSpace(userAddDto.UsersName)||string.IsNullOrWhiteSpace(userAddDto.UsersPhone)||string.IsNullOrWhiteSpace(userAddDto.UsersPwd))
            {
                msge.Code = 400;
                msge.Msg = "请填写完善对应信息";
                msge.Success = false;
                return Ok(msge);
            }
            Users users = mapper.Map<Users>(userAddDto);
            await usersService.AddEntity(users);
            await usersService.SaveChanges();
            UsersDto usersDto = mapper.Map<UsersDto>(users);
            msge.Code = 201;
            msge.Data = usersDto;
            return Ok(msge);
        }
        [HttpDelete]
        public async Task<ActionResult<UsersDto>> DeleteEntity(int? id)
        {
            MessageInfo<UsersDto> msge = new MessageInfo<UsersDto>();
            var isExist = await usersService.ExistEntity(u => u.UsersId == id);
            if (!isExist)
            {
                msge.Code = 400;
                msge.Msg = "未找到该用户";
                msge.Success = false;
                return Ok(msge);
            }
            Users user =await usersService.GetEntitys(u => u.UsersId == id).FirstOrDefaultAsync();
            usersService.DeleteEntity(user);
            await usersService.SaveChanges();
            msge.Code = 204;
            return Ok(msge);
        }
        [HttpPut]
        public async Task<ActionResult<UsersDto>> EditEntity(UsersEditDto usersEditDto)
        {
            MessageInfo<UsersDto> msge = new MessageInfo<UsersDto>();
            bool isExist = await usersService.ExistEntity(e => e.UsersId == usersEditDto.UsersId);
            if (!isExist)
            {
                msge.Code = 404;
                msge.Msg = "未找到该用户";
                msge.Success = false;
                return Ok(msge);
            }
            if (string.IsNullOrWhiteSpace(usersEditDto.UsersName))
            {
                msge.Code = 400;
                msge.Msg = "用户名是不能为空";
                msge.Success = false;
                return Ok(msge);
            }
            Users user = mapper.Map<Users>(usersEditDto);
            usersService.EditEntity(user);
            await usersService.SaveChanges();
            msge.Data = mapper.Map<UsersDto>(user);
            return Ok(msge);
        }
    }
}

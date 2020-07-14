using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PersonalDiary.Dtos;
using PersonalDiary.Models;
using PersonalDiary.Services.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PersonalDiary.Controllers
{
    [ApiController]
    [Route("api/Login")]
    public class LoginControllers: ControllerBase
    {
        private readonly IUsersService usersService;
        public LoginControllers(IUsersService usersService)
        {
            this.usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }
        [HttpPost]
        public async Task<ActionResult<MessageInfo<string>>> GetToken(LoginDto loginDto)
        {
            MessageInfo<string> msg = new MessageInfo<string>();
            if (string.IsNullOrWhiteSpace(loginDto.UsersPhone)|| string.IsNullOrWhiteSpace(loginDto.UsersPwd))
            {
                msg.Success = false;
                msg.Code = 404;
                msg.Msg = "登录名或密码不能为空";
                return Ok(msg);
            }
            else
            {
                Users user = await usersService.Login(loginDto);
                if (user == null)
                {
                    msg.Success = false;
                    msg.Code = 405;
                    msg.Msg = "用户名或密码错误";
                    return Ok(msg);
                }
                JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
                string token = securityTokenHandler.WriteToken(new JwtSecurityToken(
                      issuer: "paiguBangBang",
                      audience: "paiguSubscribe",
                      claims: new Claim[] { new Claim("usersName", user.UsersName), new Claim("usersphone", user.UsersPhone) },
                      expires: DateTime.Now.AddDays(7),
                      signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("yangtuweilaikeqi")), SecurityAlgorithms.HmacSha256)
                      ));
                string accessToken = "Bearer " + token;
                msg.Data = accessToken;
                return Ok(msg);
            }
        }
    }
}

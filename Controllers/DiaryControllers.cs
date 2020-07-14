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
    [Route("api/Diary")]
    [Authorize]
    public class DiaryControllers:ControllerBase
    {
        private readonly IDiaryService diaryService;
        private readonly IMapper mapper;

        public DiaryControllers(IDiaryService diaryService, IMapper mapper)
        {
            this.diaryService = diaryService ?? throw new ArgumentNullException(nameof(diaryService));
            this.diaryService = diaryService;
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DiaryDto>>> GetEntitys(int pageIndex, int pageSize, bool isAsc)
        {
            int totalCount = 0;
            var diarys = await diaryService.GetEntityForPage(pageIndex, pageSize, out totalCount, u => true, u => u.UsersId, isAsc).ToArrayAsync();
            if (diarys.Count() == 0)
            {
                return NotFound();
            }
            var ToDiaryDto = mapper.Map<IEnumerable<DiaryDto>>(diarys);
            return Ok(ToDiaryDto);
        }
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<DiaryDto>> GetEntitysById(int? id)
        {
            var diary = await diaryService.GetEntitys(u => u.DiaryId == id && u.IsPublic ==true).FirstOrDefaultAsync();
            if (diary == null)
            {
                return NotFound();
            }
            else
            {
                var ToDiaryDto = mapper.Map<DiaryDto>(diary);
                return Ok(ToDiaryDto);
            }
        }
        [HttpPost]
        public async Task<ActionResult<DiaryDto>> AddEntity(DiaryAddDto diaryAddDto)
        {
            MessageInfo<DiaryDto> msge = new MessageInfo<DiaryDto>();
            if (string.IsNullOrWhiteSpace(diaryAddDto.DiaryTitle) || string.IsNullOrWhiteSpace(diaryAddDto.DiaryTitleContent))
            {
                msge.Code = 400;
                msge.Msg = "请填写完善对应信息";
                msge.Success = false;
                return Ok(msge);
            }
            Diary diary = mapper.Map<Diary>(diaryAddDto);
            await diaryService.AddEntity(diary);
            await diaryService.SaveChanges();
            DiaryDto toDiaryDto = mapper.Map<DiaryDto>(diary);
            msge.Code = 201;
            return Ok(toDiaryDto);
        }
        [HttpDelete]
        [Route("{UserId}")]
        public async Task<ActionResult<DiaryDto>> DeleteEntity(int? id,int? UserId)
        {
            MessageInfo<DiaryDto> msge = new MessageInfo<DiaryDto>();
            var isExist = await diaryService.ExistEntity(u => u.DiaryId == id && u.UsersId==UserId);
            if (!isExist)
            {
                msge.Code = 400;
                msge.Msg = "未找到该用户";
                msge.Success = false;
                return Ok(msge);
            }
            Diary diary = await diaryService.GetEntitys(u => u.DiaryId == id).FirstOrDefaultAsync();
            diaryService.DeleteEntity(diary);
            await diaryService.SaveChanges();
            msge.Code = 204;
            return Ok(msge);
        }
        [HttpPut]
        public async Task<ActionResult<DiaryDto>> EditEntity(DiaryEditDto diaryEditDto)
        {
            MessageInfo<DiaryDto> msge = new MessageInfo<DiaryDto>();
            bool isExist = await diaryService.ExistEntity(e => e.DiaryId == diaryEditDto.DiaryId);
            if (!isExist)
            {
                msge.Code = 404;
                msge.Msg = "未找到该用户";
                msge.Success = false;
                return Ok(msge);
            }
            if (string.IsNullOrWhiteSpace(diaryEditDto.DiaryTitle) || string.IsNullOrWhiteSpace(diaryEditDto.DiaryTitleContent))
            {
                msge.Code = 400;
                msge.Msg = "标题和内容不能为空";
                msge.Success = false;
                return Ok(msge);
            }
            Diary diary = mapper.Map<Diary>(diaryEditDto);
            diaryService.EditEntity(diary);
            await diaryService.SaveChanges();
            msge.Data = mapper.Map<DiaryDto>(diary);
            return Ok(msge);
        }
    }
}

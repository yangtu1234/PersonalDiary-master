using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalDiary.Dtos
{
    public class DiaryAddDto
    {
        public string DiaryTitle { get; set; }
        public string DiaryTitleContent { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsPublic { get; set; }
        public string DiaryType { get; set; }
        public int? UsersId { get; set; }
    }
}

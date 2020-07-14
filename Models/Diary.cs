using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalDiary.Models
{
    public partial class Diary
    {
        public int DiaryId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "长度必须在1-50字符之间！")]
        public string DiaryTitle { get; set; }
        [Required]
        [StringLength(4000,MinimumLength =4,ErrorMessage = "长度必须在4-4000字符之间！")]
        public string DiaryTitleContent { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsPublic { get; set; }
        public string DiaryType { get; set; }
        public int? UsersId { get; set; }

        public virtual Users Users { get; set; }
    }
}

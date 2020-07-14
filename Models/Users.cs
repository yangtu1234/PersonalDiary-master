using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalDiary.Models
{
    public partial class Users
    {
        public Users()
        {
            Diary = new HashSet<Diary>();
        }
        public int UsersId { get; set; }
        [Required]
        [StringLength(50,MinimumLength =1,ErrorMessage ="用户名长度1-50个字符之间")]
        public string UsersName { get; set; }
        public string UsersPhoto { get; set; }
        public string UsersPhone { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码长度6-20个字符之间")]
        public string UsersPwd { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<Diary> Diary { get; set; }
    }
}

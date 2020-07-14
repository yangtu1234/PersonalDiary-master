using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalDiary.Dtos
{
    public class UsersDto
    {
        public int UsersId { get; set; }
        public string UsersName { get; set; }
        public string UsersPhoto { get; set; }
        public string UsersPhone { get; set; }
        //public string UsersPwd { get; set; }
        public string Remark { get; set; }
    }
}

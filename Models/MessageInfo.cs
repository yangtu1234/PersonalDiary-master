using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalDiary.Models
{
    public class MessageInfo<T> where T : class
    {
        public bool Success { get; set; } = true;
        public int Code { get; set; } = 200;
        public string Msg { get; set; } = "成功！！！";
        public T Data { get; set; }
    }
}

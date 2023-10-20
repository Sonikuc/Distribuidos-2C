using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Requests
{
    public class UserRequest
    {
        public string? name { get; set; }
        public List<string>? emails { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<string>? Emails { get; set; }
    }
}

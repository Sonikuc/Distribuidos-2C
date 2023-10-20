using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Responses
{
    public class GetUsersResponse
    {
        public int count { get; set; }
        public List<User>results {get; set;}
    }
}

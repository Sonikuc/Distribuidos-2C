using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Queries
{
    public class GetUserByIdQuery : IRequest<UserResponse>
    {
        public int UserId { get; set; }
        public GetUserByIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}

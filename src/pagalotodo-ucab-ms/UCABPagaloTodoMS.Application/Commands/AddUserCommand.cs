using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class AddUserCommand : IRequest <UserRequest>
    {
        public UserRequest _request;
        public AddUserCommand(UserRequest request)
        {
            _request = request;
        }
    }
}

﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class PutUserCommand : IRequest<UserResponse>
    {
        public UserRequest _request { get; set; }
        public int UserId { get; set; }
        public PutUserCommand(UserRequest user, int id)
        {
            _request = user;
            UserId = id;
        }
    }
}

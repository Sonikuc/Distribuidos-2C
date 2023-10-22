﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Responses;

namespace UCABPagaloTodoMS.Application.Commands
{
    public class DeleteUserCommand : IRequest<string>
    {
        public int UserId { get; set; }
        public DeleteUserCommand(int userId)
        {
            UserId = userId;
        }

    }
}

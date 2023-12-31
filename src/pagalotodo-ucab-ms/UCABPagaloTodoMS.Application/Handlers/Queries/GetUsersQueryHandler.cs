﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Queries;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<GetUsersQueryHandler> _logger;

        public GetUsersQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<GetUsersQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<GetUsersResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("ConsultarValoresQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return HandleAsync();
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("ConsultarValoresQueryHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private async Task<GetUsersResponse> HandleAsync()
        {
            try
            {
                _logger.LogInformation("ConsultarValoresQueryHandler.HandleAsync");

                var result = _dbContext.User.Include(u => u.Emails).ToList();
                var response = new GetUsersResponse { count = result.Count(), results = new List<UserResponse>() };
                foreach(var item in result ) 
                {
                    var user = new UserResponse
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Emails = new List<string>()
                    };
                    if (item.Emails.Count() > 0)
                        foreach (var email in item.Emails) 
                        {
                            if(email.Email != null)
                            user.Emails.Add(email.Email);
                        }
                    response.results.Add(user);
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                throw;
            }
        }
    }

}

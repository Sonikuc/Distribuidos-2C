using MediatR;
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
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;

        public GetUserByIdQueryHandler(IUCABPagaloTodoDbContext dbContext, ILogger<GetUserByIdQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
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
                    return HandleAsync(request);
                }
            }
            catch (Exception)
            {
                _logger.LogWarning("ConsultarValoresQueryHandler.Handle: ArgumentNullException");
                throw;
            }
        }

        private async Task<UserResponse> HandleAsync(GetUserByIdQuery request)
        {
            try
            {
                _logger.LogInformation("ConsultarValoresQueryHandler.HandleAsync");

                var result = _dbContext.User.Include(u => u.Emails).FirstOrDefault(c=>c.Id == request.UserId);
                if (result == null) 
                {
                    throw new ArgumentNullException(nameof(result));
                }
                var response = new UserResponse
                {
                    Id = result.Id,
                    Name = result.Name,
                    Emails = new List<string>()
                };
                foreach (var email in result.Emails) 
                {
                    response.Emails.Add(email.Email);
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

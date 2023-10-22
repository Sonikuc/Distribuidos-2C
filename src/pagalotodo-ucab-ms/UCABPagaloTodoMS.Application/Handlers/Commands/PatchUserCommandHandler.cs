using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    internal class PatchUserCommandHandler : IRequestHandler<PatchUserCommand, UserResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<PatchUserCommandHandler> _logger;

        public PatchUserCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<PatchUserCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<UserResponse> Handle(PatchUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("ConsultarValoresQueryHandler.Handle: Request nulo.");
                    throw new ArgumentNullException(nameof(request));
                }
                else
                {
                    return await HandleAsync(request);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<UserResponse> HandleAsync(PatchUserCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                var user = _dbContext.User.Include(c => c.Emails).FirstOrDefault(c => c.Id == request.UserId);
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }
                user.Name = request._request.name;
                _logger.LogInformation("AddUserCommandHandler.HandleAsync {Request}", request);
                _dbContext.User.Update(user);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AddUserCommandHandler.HandleAsync {Response}", request._request);
                var response = new UserResponse
                {
                    Id = user.Id,
                    Name = request._request.name,
                    Emails = new List<string>()
                };
                if (user.Emails.Count() > 0)
                {
                    foreach (var email in user.Emails)
                    {
                        response.Emails.Add(email.Email);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConsultarValoresQueryHandler.HandleAsync. {Mensaje}", ex.Message);
                transaccion.Rollback();
                throw;
            }
        }

    }
}

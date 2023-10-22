using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    internal class PutUserCommandHandler : IRequestHandler<PutUserCommand, UserResponse>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<PutUserCommandHandler> _logger;

        public PutUserCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<PutUserCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<UserResponse> Handle(PutUserCommand request, CancellationToken cancellationToken)
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

        private async Task<UserResponse> HandleAsync(PutUserCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                var user = _dbContext.User.Include(c => c.Emails).FirstOrDefault(c => c.Id == request.UserId);
                if (user == null) 
                {
                    throw new ArgumentNullException(nameof(request));
                }
                if (user.Emails.Count() > 0)
                    foreach (var useremails in user.Emails) 
                    {
                        _dbContext.Emails.Remove(useremails);
                    }
                user.Name = request._request.name;
                foreach (var email in request._request.emails)
                {
                    var entity2 = new Emails { Email = email, UserId = user.Id };
                    _dbContext.Emails.Add(entity2);
                }
                _logger.LogInformation("AddUserCommandHandler.HandleAsync {Request}", request);
                _dbContext.User.Update(user);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AddUserCommandHandler.HandleAsync {Response}", request._request);
                var response = new UserResponse 
                {
                    Id = user.Id,
                    Name = request._request.name,
                    Emails = request._request.emails,
                };
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

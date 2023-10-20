using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Handlers.Queries;
using UCABPagaloTodoMS.Application.Mappers;
using UCABPagaloTodoMS.Application.Requests;
using UCABPagaloTodoMS.Core.Database;
using UCABPagaloTodoMS.Core.Entities;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    internal class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserRequest>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<AddUserCommandHandler> _logger;

        public AddUserCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<AddUserCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<UserRequest> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request._request == null)
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

        private async Task<UserRequest> HandleAsync(AddUserCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                var entity = new User { Name = request._request.name};
                foreach (var email in request._request.emails) 
                {
                    var entity2 = new Emails { Email = email };
                    _dbContext.Emails.Add(entity2);
                }
                _logger.LogInformation("AddUserCommandHandler.HandleAsync {Request}", request);
                _dbContext.User.Add(entity);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AddUserCommandHandler.HandleAsync {Response}", request._request);
                return request._request;
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

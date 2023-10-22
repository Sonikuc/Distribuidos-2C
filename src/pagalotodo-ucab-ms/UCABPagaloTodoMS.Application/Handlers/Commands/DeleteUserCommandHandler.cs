using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Application.Commands;
using UCABPagaloTodoMS.Application.Responses;
using UCABPagaloTodoMS.Core.Database;

namespace UCABPagaloTodoMS.Application.Handlers.Commands
{
    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IUCABPagaloTodoDbContext dbContext, ILogger<DeleteUserCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
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

        private async Task<string> HandleAsync(DeleteUserCommand request)
        {
            var transaccion = _dbContext.BeginTransaction();
            try
            {
                var user = _dbContext.User.FirstOrDefault(c => c.Id == request.UserId);
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }               
                _dbContext.User.Remove(user);
                await _dbContext.SaveEfContextChanges("APP");
                transaccion.Commit();
                _logger.LogInformation("AddUserCommandHandler.HandleAsync {Response}", request.UserId);
                return "Deleted";
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

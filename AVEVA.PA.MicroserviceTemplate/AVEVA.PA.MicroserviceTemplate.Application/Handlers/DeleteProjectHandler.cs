using MediatR;
using AVEVA.PA.MicroserviceTemplate.Core.IConfiguration;
using AVEVA.PA.MicroserviceTemplate.Application.Commands;
using Microsoft.Extensions.Logging;

namespace AVEVA.PA.MicroserviceTemplate.Application.Handlers
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand,bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<DeleteProjectHandler> logger;
        public DeleteProjectHandler(IUnitOfWork unitOfWork, ILogger<DeleteProjectHandler> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }
        public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {

            this.logger.LogInformation($"Delete project request with id {request.Id} recieved");
            await this.unitOfWork.Projects.DeleteAsync(request.Id);
            await this.unitOfWork.SaveAsync();
            return true;
        }
       
    }
}

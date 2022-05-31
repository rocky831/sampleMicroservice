using AVEVA.PA.DataAccess.Models;
using AVEVA.PA.MicroserviceTemplate.Core.IConfiguration;
using AVEVA.PA.Utility;
using MassTransit;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AVEVA.PA.MicroserviceTemplate.Infrastructure.MessageConsumer
{
    public class ProjectConsumer : IConsumer<Project>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<ProjectConsumer> logger;
        private readonly IMemoryCache memoryCache;

        public ProjectConsumer(IUnitOfWork unitOfWork, ILogger<ProjectConsumer> logger, IMemoryCache memoryCache)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.memoryCache = memoryCache;
        }

        public async Task Consume(ConsumeContext<Project> context)
        {
            try
            {
                await unitOfWork.Projects.AddAsync(context.Message);
                await unitOfWork.SaveAsync();
                memoryCache.Set(context.Message.ProjectGuid, "Processed");
            }catch (Exception exception)
            {
               
                exception.Data.Add(Constants.TraceIdentifierName, context.MessageId.ToString());
                this.logger.LogError(exception, $"Something went wrong while consuming messages, error:{exception.Message}");
            }
           
        }
    }
}

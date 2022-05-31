using AVEVA.PA.MicroserviceTemplate.Application.Commands;
using AVEVA.PA.MicroserviceTemplate.Application.Dtos;
using AVEVA.PA.MicroserviceTemplate.Application.Queries;
using AVEVA.PA.Exceptions;
using AVEVA.PA.MicroserviceTemplate.Web.Exceptions;
using AVEVA.PA.Utility.Utilities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AVEVA.PA.MicroserviceTemplate.Web.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<ProjectController> logger;
        private readonly IMemoryCache memoryCache;

        public ProjectController(IMediator mediator, ILogger<ProjectController> logger, IMemoryCache memoryCache)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.memoryCache = memoryCache;
        }

        /// <summary>
        /// Gets all projects
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task <ActionResult<IEnumerable<ReadProjectDto>>> Get(CancellationToken cancellationToken)
        {
            var query = new GetAllProjectsQuery();
            var response = await mediator.Send(query, cancellationToken);

            return Ok(response);              
        }

        /// <summary>
        /// Gets projects by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadProjectDto>> Get(int id, CancellationToken cancellationToken)
        {
            var query = new GetProjectByIdQuery(id);
            var response = await mediator.Send(query, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Add new project
        /// </summary>
        /// <param name="project"></param>
        /// <param name="cancellationToken"></param>
        ///  <param name="culture"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult> AddProject([FromBody] CreateProjectDto project, CancellationToken cancellationToken, [FromHeader(Name = "culture")] string culture)
        {
           
                logger.LogInformation($"The project {project.AssetId} is recieved");
                var acceptedReq = new CreateProjectCommand()
                {
                    Project=project
                };
                var result = await mediator.Send(acceptedReq, cancellationToken);
                logger.LogInformation("The project {AssetID} is accepted", project.AssetId);
                return Accepted("Project Accepted", result); 
        }

        // PUT api/<ProjectController>/5
        /// <summary>
        /// Updates project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="project"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadProjectDto>> Put(int id, [FromBody] UpdateProjectDto project, CancellationToken cancellationToken)
        {
            var updateRequest = new UpdateProjectCommand(id, project);
            return await mediator.Send(updateRequest, cancellationToken);
        }

        // DELETE api/<ProjectController>/5
        /// <summary>
        /// Delete project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var deleteReq = new DeleteProjectCommand() { Id = id };
            await mediator.Send(deleteReq, cancellationToken);
            return Ok(id);
        }


        /// <summary>
        /// Gets project status by projectGuid
        /// </summary>
        /// <param name="projectGuid"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("status/{projectGuid}")]
        public async Task<IActionResult> GetProjectStaus(string projectGuid, CancellationToken cancellationToken)
        {
     
                memoryCache.TryGetValue(projectGuid, out string status);
                return Ok(status);
        }
    }
}

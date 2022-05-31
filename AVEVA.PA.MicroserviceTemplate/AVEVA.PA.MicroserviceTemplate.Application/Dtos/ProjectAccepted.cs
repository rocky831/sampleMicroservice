namespace AVEVA.PA.MicroserviceTemplate.Application.Dtos
{
    public class ProjectAccepted
    {
        public ProjectAccepted(string projectGuid)
        {
            this.ProjectGuid = projectGuid;
        }
        public string ProjectGuid { get; }
    }
}

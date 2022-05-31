using AVEVA.PA.Exceptions;
using Newtonsoft.Json;

namespace AVEVA.PA.MicroserviceTemplate.Web.Exceptions
{
    public class ProjectFailedToCreateException : ProjectException
    {
        public ProjectFailedToCreateException(object projectObj, Exception reasonException)
            : base("FailedToCreate", $"Failed to create project. See the Data property for details", reasonException)
        {
            // pass unsecured values to exception translation
            var json = JsonConvert.SerializeObject(projectObj);
            this.Data["projectJson"] = json;
        }
    }
}
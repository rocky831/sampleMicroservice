using AVEVA.PA.Exceptions;

namespace AVEVA.PA.Exceptions
{
    public class ProjectNotFoundException : ProjectException
    {
        public ProjectNotFoundException(int projectID)
            : base("NotFound", $"Project with ID '{projectID}' is not found.")
        {
            // pass unsecured values to exception translation
            this.Data["projectID"] = projectID;
        }
    }
}
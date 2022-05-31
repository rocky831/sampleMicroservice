namespace AVEVA.PA.Exceptions
{
    public class ProjectsNotFoundException : ProjectException
    {
        public ProjectsNotFoundException()
            : base("NoProjects", $"There are no projects found.")
        {
        }
    }
}
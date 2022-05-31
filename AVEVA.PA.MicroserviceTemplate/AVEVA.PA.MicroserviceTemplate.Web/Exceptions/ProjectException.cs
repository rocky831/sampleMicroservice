namespace AVEVA.PA.Exceptions
{
    public class ProjectException : BaseException
    {
        public ProjectException(ErrorCode code, string message, Exception innerException = null)
            : base("Proj" + code, message, innerException) { }
    }
}
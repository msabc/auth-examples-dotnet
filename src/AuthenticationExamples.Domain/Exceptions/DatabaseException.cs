namespace BasicAuth.Domain.Exceptions
{
    public class DatabaseException(
        string message, 
        string methodName, 
        string repositoryName, 
        Exception innerException) : Exception(message, innerException)
    {
        public string MethodName { get; internal set; } = methodName;

        public string RepositoryName { get; internal set; } = repositoryName;
    }
}

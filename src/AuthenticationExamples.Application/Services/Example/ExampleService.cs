namespace AuthenticationExamples.Application.Services.Example
{
    public class ExampleService : IExampleService
    {
        public IEnumerable<int> ExampleGet()
        {
            return [1, 2, 3];
        }
    }
}

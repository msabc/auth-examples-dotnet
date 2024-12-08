namespace AuthenticationExamples.Application.Exceptions
{
    public record BasicAuthExceptionMessage
    {
        public required string ErrorMessage { get; set; }
    }
}

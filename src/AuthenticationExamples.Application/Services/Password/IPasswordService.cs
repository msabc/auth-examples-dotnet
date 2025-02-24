namespace AuthenticationExamples.Application.Services.Password
{
    public interface IPasswordService
    {
        (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string input);

        bool VerifyPassword(string input, byte[] passwordHash, byte[] passwordSalt);
    }
}

using System.ComponentModel.DataAnnotations;

namespace AuthenticationExamples.Application.Models.Request.BasicAuthentication
{
    public record RegisterRequest
    {
        [Required]
        [StringLength(50, ErrorMessage = "Username field length does not conform to the expected standard.", MinimumLength = 5)]
        public required string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "E-mail address is in an invalid format.")]
        [StringLength(100, ErrorMessage = "E-mail address field length does not conform to the expected standard.", MinimumLength = 5)]
        public required string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Password field length does not conform to the expected standard.", MinimumLength = 5)]
        public required string Password { get; set; }
    }
}

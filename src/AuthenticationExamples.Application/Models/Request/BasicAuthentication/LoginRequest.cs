﻿namespace AuthenticationExamples.Application.Models.Request.BasicAuthentication
{
    public record LoginRequest
    {
        public required string Username { get; set; }

        public required string Password { get; set; }
    }
}

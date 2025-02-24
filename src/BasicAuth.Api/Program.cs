using AuthenticationExamples.IoC;
using BasicAuth.Api.Authentication;
using BasicAuth.Api.Exceptions;
using BasicAuth.Api.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;

const string BasicAuthenticationSchemeName = "BasicAuthentication";

var builder = WebApplication.CreateBuilder(args);

// Register Basic Authentication
builder.Services.AddAuthentication(BasicAuthenticationSchemeName)
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(BasicAuthenticationSchemeName, null);

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("basic auth", new OpenApiSecurityScheme()
    {
        Description = "Basic authorization header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "basic"
    });

    options.OperationFilter<BasicAuthenticationOperationFilter>();
});

builder.Services.RegisterApplicationDependencies(builder.Configuration);

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.yaml", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();

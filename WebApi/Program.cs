using Application.Common.Extensions;
using Infrastructure.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.

    builder.Services.AddControllers();
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    builder.Services.AddHealthChecks();

    builder.Services.AddApplicationDependency();
    builder.Services.AddInfrastructureDependency(builder.Configuration);
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseMiddleware<ExceptionMiddleware>();
    app.UseHealthChecks("/health");

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
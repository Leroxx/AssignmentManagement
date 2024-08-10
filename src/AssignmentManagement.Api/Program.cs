using AssignmentManagement.Api.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddEndpointsApiExplorer()
        .AddServices()
        .AddProblemDetails()
        .AddControllers();
}

var app = builder.Build();
{
    app.UseExceptionHandler();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
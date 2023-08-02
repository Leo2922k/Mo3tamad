using API.Date;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

// middleware
app.UseAuthentication(); // do u have valid token? 
app.UseAuthorization(); // what are u allowed to do?

app.MapControllers();

using var scope = app.Services.CreateScope(); // access to all services in the program class

var services = scope.ServiceProvider; 

try {
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
    await Seed.SeedExams(context);
    await Seed.SeedUserExams(context);
}
catch (Exception ex) {

    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");

}
app.Run();

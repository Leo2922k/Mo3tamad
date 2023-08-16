using API.Date;
using API.Entities;
using API.Extensions;
using API.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
//1
builder.Services.AddCoreAdmin();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

//app.UseCoreAdminCustomAuth((serviceProvider) => Task.FromResult(false));

// middleware
app.UseAuthentication(); // do u have valid token? 
app.UseAuthorization(); // what are u allowed to do?

app.MapControllers();
//2
app.UseStaticFiles();

using var scope = app.Services.CreateScope(); // access to all services in the program class

var services = scope.ServiceProvider; 


try {
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(userManager, roleManager);
    await Seed.SeedExams(context);
    await Seed.SeedUserExams(context);
}
catch (Exception ex) {

    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");

}
//3
app.MapDefaultControllerRoute();
 

app.Run();

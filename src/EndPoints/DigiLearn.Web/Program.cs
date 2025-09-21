using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using TIcketModules;
using UserModule.Core.Queries._DTOs;
using UserModule.Core.Services;
using UserModules.Core.Commands.Users.Register;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(Program).Assembly,
        typeof(RegisterUserCommandHandler).Assembly,
        typeof(UserDto).Assembly,
        typeof(UserFacade).Assembly,
        typeof(IUserFacade).Assembly
    );
});

builder.Services.AddScoped<ILocalFileService, LocalFileService>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services
    .InitUsertModule(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

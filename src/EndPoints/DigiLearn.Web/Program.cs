using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using DigiLearn.Web.Infrastructure.JwtUtil;
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

var services = builder.Services;
builder.Services.AddScoped<ILocalFileService, LocalFileService>();

// Add services to the container.
builder.Services
    .AddRazorPages()
    .AddRazorRuntimeCompilation();
builder.Services
    .InitUsertModule(builder.Configuration)
    .InitTicketModule(builder.Configuration);

services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    var token = context.Request.Cookies["digi-token"]?.ToString();
    if (string.IsNullOrWhiteSpace(token) == false)
    {
        context.Request.Headers.Append("Authorization", $"Bearer {token}");
    }
    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

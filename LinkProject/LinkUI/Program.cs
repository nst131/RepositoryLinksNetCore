using LinkBL;
using LinkDL;
using LinkUI;
using LinkUI.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddNewtonsoftJson();

builder.Services.AddRegistrationDL(
    Environment.GetEnvironmentVariable("Connection_String") ??
    builder.Configuration.GetConnectionString("DefaultConnection") ?? "");

builder.Services.AddRegistrationBL();
builder.Services.AddAutoMapper(typeof(MapperConfigurationBL), typeof(MapperConfigurationUI));

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();
app.UseMiddleware<ErrorHandleMiddleware>();

await app.InitialMigration(
     Environment.GetEnvironmentVariable("Connection_String") ??
    builder.Configuration.GetConnectionString("DefaultConnection") ?? "");

app.UseRouting();

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllers();

app.Run();

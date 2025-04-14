using allocation.Controllers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddControllersWithViews();

var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ??
                       builder.Configuration.GetConnectionString("DevDB");

var usePostgreSql = Environment.GetEnvironmentVariable("USE_POSTGRESQL") == "true" ||
                    !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DATABASE_URL"));


builder.Services.AddDbContext<AppDbContext>(options =>
{
    // if (usePostgreSql)
    {

        options.UseNpgsql(connectionString);
    }

    /*  else
      {

          options.UseSqlServer(connectionString);
      }
    */
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
  
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
if (app.Environment.IsDevelopment())
{

    app.Run();
}
else
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    app.Run($"http://0.0.0.0:{port}");
}
using Logic.TechnicalAssement.Core.Extensions;
using Logic.TechnicalAssement.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Although brief mentions to do this in session, I dont feel that this will give you
// enough of an insight into how I would tyically handle data
builder.Services.AddDatabase();
builder.Services.AddApplicationServices();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

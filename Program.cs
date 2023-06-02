using Craftfest;
using Craftfest.Areas.Identity;
using Craftfest.Data;
using Craftfest.Hubs;
using Craftfest.Interfaces;
using Craftfest.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Environment.IsDevelopment() ?
    builder.Configuration.GetConnectionString("Craftfest")
    : Startup.GetHerokuConnectionString();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        //if (builder.Environment.IsDevelopment())
        //{
            options.UseNpgsql(connectionString);
        //}
        //else
        //{
        //    options.UseNpgsql(connectionString);
        //}

    });


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<IBracketRepo, BracketRepo>();
builder.Services.AddScoped<ITournamentRepo, TournamentRepo>();
builder.Services.AddScoped<IGameRepo, GameRepo>();
builder.Services.AddScoped<ITeamRepo, TeamRepo>();
builder.Services.AddScoped<TournamentService>();
builder.Services.AddResponseCompression(opt =>
{
    opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});

builder.Services.AddMudServices();

var app = builder.Build();

string? port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrWhiteSpace(port)) 
{ 
    app.Urls.Add("http://*:" + port); 
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapHub<ChatHub>("/chathub");
app.MapHub<TournamentHub>("/tournamenthub");
app.MapFallbackToPage("/_Host");

//using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
//{
//    var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    context.Database.Migrate();
//}

app.Run();



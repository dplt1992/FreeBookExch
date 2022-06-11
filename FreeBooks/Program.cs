// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// :::::: I N S T I T U T O :: P O L I T É C N I C O :: D E :: T O M A R ::::::
// ::::::::::::::::: E N G E N H A R I A :: I N F O R M Á T I C A :::::::::::::
// :::::::::::::: D E S E N V O L V I M E N T O :: W E B :: 2021/2022 :::::::::
// ::::::::::::::::::::::::::::::: Copyright(C) :::::::::::::::::::::::::::::::
// :::::::::: aluno19169@ipt.pt :::::::::::::: aluno21425@ipt.pt ::::::::::::::
// ::: https://github.com/dplt1992 https://github.com/Flavio-Oliveira-21425 :::
// ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// ////////////////////////////////////////////////////////////////////////////

using FreeBooks.Data;
using FreeBooks.Infrastructure;
using FreeBooks.Models;
using FreeBooks.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// acesso á base de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

/*::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
// acesso à autenticação
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>(); ;
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
/*::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

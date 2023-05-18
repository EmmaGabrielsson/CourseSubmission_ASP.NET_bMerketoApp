using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebbApp.Contexts;
using WebbApp.Models.Identities;
using WebbApp.Repositories;
using WebbApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//contexts
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlDataDb")));
builder.Services.AddDbContext<IdentityContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlIdentityDb")));

//repositories
builder.Services.AddScoped<ContactFormRepo>();
builder.Services.AddScoped<CategoryRepo>();
builder.Services.AddScoped<ProductCategoryRepo>();
builder.Services.AddScoped<ProductRepo>();
builder.Services.AddScoped<StockRepo>();
builder.Services.AddScoped<ProductReviewRepo>();
builder.Services.AddScoped<TagRepo>();
builder.Services.AddScoped<ProductTagRepo>();
builder.Services.AddScoped<ShowcaseRepo>();
builder.Services.AddScoped<SubscribeRepo>();
builder.Services.AddScoped<UserAdressRepo>();
builder.Services.AddScoped<AdressRepo>();
builder.Services.AddScoped<UserRepo>();
builder.Services.AddScoped<CollectionRepo>();

//services
builder.Services.AddScoped<SeedService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<AdressService>();

//identities
builder.Services.AddIdentity<AppUser, IdentityRole>(x =>
{
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
    x.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<IdentityContext>();


var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

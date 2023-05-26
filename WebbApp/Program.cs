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

//identities
builder.Services.AddIdentity<AppUser, IdentityRole>(x =>
{
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
    x.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<IdentityContext>();

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
builder.Services.AddScoped<CollectionRepo>();
builder.Services.AddScoped<OrderRepo>();
builder.Services.AddScoped<OrderRowRepo>();

//services
builder.Services.AddScoped<SeedService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<AdressService>();
builder.Services.AddScoped<OrderService>();

//sessions
builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromHours(5);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });



var app = builder.Build();
app.UseHsts();
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

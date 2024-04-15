using LIbrary.Data;
using LIbrary.Models;
using LIbrary.Repository.Specific;
using LIbrary.Services.BookCatalogue;
using LIbrary.Services.FineReader;
using LIbrary.Services.Payment;
using LIbrary.Services.Reminder;
using LIbrary.Services.ReturnBook;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// With Razor runtime compilation enabled, changes made to Razor views (.cshtml files) are automatically recompiled without requiring a manual build or restart of the application. 
builder.Services.AddMvc().AddRazorRuntimeCompilation();

// This method is an extension method provided by ASP.NET Core. It registers the IMemoryCache interface and its implementation with the dependency injection container, making it available for injection into other components of your application.
builder.Services.AddMemoryCache();

#region DataAccess
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDbContext<AppDbContext>(option => option.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region Repository
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IReviewRatingRepository, ReviewRatingRepository>();
builder.Services.AddScoped<IBookRepository,BookRepository>();
builder.Services.AddScoped<IBookCopyRepository,BookCopyRepository>();
builder.Services.AddScoped<IBorrowItemRepository, BorrowItemRepository>();
builder.Services.AddScoped<IBorrowItemStatusRepository,BorrowItemStatusRepository>();
builder.Services.AddScoped<IGenreRepository,GenreRepository>();
builder.Services.AddScoped<IReaderRepository, ReaderRepository>();
builder.Services.AddScoped<IFineRepository, FineRepository>();
builder.Services.AddScoped<IFineStatusRepository, FineStatusRepository>();
#endregion

#region Service
builder.Services.AddScoped<IBookCatalogueService, BookCatalogueService>();
builder.Services.AddScoped<IBorrowBookService, BorrowBookService>();
builder.Services.AddScoped<IReturnBookService, ReturnBookService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IFineService, FineService>();
builder.Services.AddScoped<IReminderService, ReminderService>();
builder.Services.AddScoped<IEmailSender,EmailSender>();
#endregion

#region hosted service
//builder.Services.AddHostedService<BackgroundRefresh>();
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

#region Authentication
builder.Services.AddIdentity<Reader, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});
#endregion

#region Authorization
// add role reader
builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("Reader",
        authBuilder =>
        {
            authBuilder.RequireRole("Reader");
        });

});
#endregion

#region Claims
// Claims are a way to retrieve current logged in (in session) user credentials like id or username
builder.Services.AddScoped<IUserClaimsPrincipalFactory<Reader>, ApplicationUserClaimsPrincipalFactory>();
#endregion

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// The app.UseStaticFiles() method configures the middleware pipeline to serve static files, such as HTML, CSS, JavaScript, and image files, from the application's wwwroot directory or any other specified directory.
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
AppDbInitializer.Seed(app);

app.Run();

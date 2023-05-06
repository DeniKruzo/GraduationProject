using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GraduationProject.Data;
using GraduationProject.Areas.Identity.Data;
using GraduationProject.Abstract;
using GraduationProject.mocks;
using GraduationProject.Data.Repository;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GraduationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'GraduationDbContextConnection' not found.");

builder.Services.AddDbContext<GraduationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<GraduationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

//подключаем репу через синглтон
builder.Services.AddTransient<IAllOrders,OrderRepository>();
builder.Services.AddTransient<IOrderCategory, CategoryOfOrderRepository>();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();



using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GraduationDbContext>();
    //заполнили тестовыми данными бд
    DataHelper.Seed(context);
}


app.Run();

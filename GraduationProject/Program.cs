using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GraduationProject.Data;
using GraduationProject.Areas.Identity.Data;
using GraduationProject.Abstract;
using GraduationProject.mocks;
using GraduationProject.Data.Repository;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.AspNetCore.SignalR;
using GraduationProject.Controllers;
using GraduationProject.Hubs;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GraduationDbContextConnection") ?? 
    throw new InvalidOperationException("Connection string 'GraduationDbContextConnection' not found.");

builder.Services.AddDbContext<GraduationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<GraduationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSignalR();

//���������� ���� ����� ��������
builder.Services.AddTransient<IAllOrders,OrderRepository>();
builder.Services.AddTransient<IOrderCategory, CategoryOfOrderRepository>();
builder.Services.AddTransient<IGetProfiles, ProfileRepository>();
builder.Services.AddTransient<IHaveSpecialization, SpecializationRepository>();
builder.Services.AddTransient<IViewComments, CommentRepository>();
builder.Services.AddTransient<IMakeResponse, ResponseRepository>();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();;
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

   endpoints.MapHub<ChatHub>("/chatHub");
});
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GraduationDbContext>();
    //��������� ��������� ������� ��
    DataHelper.Seed(context);
}

app.Run();

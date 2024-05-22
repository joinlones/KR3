using Microsoft.EntityFrameworkCore;
using UniversityApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//����������� ��
builder.Services.AddDbContext<UniversityContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("UniversityContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/university/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//������������� �� � ������ ������� � ����������� ������, ���� �� ������
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// ��������� �������� �� ��������� ����� app.Map()
app.Map("/", () => Results.Redirect("/studs/Index"));

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=studs}/{action=Index}");

app.Run();

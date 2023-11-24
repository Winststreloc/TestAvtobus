
using TestAvtobus.Interfaces;
using TestAvtobus.Models.NHibernate;
using TestAvtobus.Repository;
using TestAvtobus.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ILinkShortenerService, LinkShortenerService>();
builder.Services.AddScoped<IRecordUrlRepository, RecordUrlRepository>();
builder.Services.AddScoped<ICounterLinkService, CounterLinkService>();

builder.Services.AddSingleton(NHibernateHelper.OpenSession());


builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Record}/{action=GetAllRecords}/{id?}");

app.Run();
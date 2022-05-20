using GlonasssoftTestWebApi.Db;
using GlonasssoftTestWebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


ConfigureServices(builder.Services);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    //настройка MySQL сервера
    var db = builder.Configuration.GetValue<string>("Database");
    services.AddDbContext<ApplicationDatabaseContext>(options => options.UseMySql(db, ServerVersion.AutoDetect(db)));

    services.AddTransient<ReportsService>();
    services.AddTransient<UsersService>();

}

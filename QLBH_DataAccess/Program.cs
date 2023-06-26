

using Microsoft.EntityFrameworkCore;
//using QLBH_DataAccess;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<QLBH_ONLINEContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr"));
//});

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<QLBH_ONLINEContext>();
//    db.Database.Migrate();
//}

app.MapGet("/", () => "Hello World!");

app.Run();

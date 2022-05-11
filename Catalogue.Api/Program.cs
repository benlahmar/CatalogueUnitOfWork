using Catalogue.Core.Interfaces;
using Catalogue.EF;
using Catalogue.EF.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var conf=builder.Configuration.AddJsonFile("appsettings.json").Build();
// Add services to the container.
   
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(
        options => options.UseSqlServer(conf.GetConnectionString("Strconn"),
        b=>b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


builder.Services.AddTransient(typeof(IBaseRepository<>),typeof(BaseRepository <>));

builder.Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

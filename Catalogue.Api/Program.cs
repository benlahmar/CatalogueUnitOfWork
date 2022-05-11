using Catalogue.Core.Interfaces;
using Catalogue.EF;
using Catalogue.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Steeltoe.Discovery.Client;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.Management.Endpoint;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddConfigServer();


var conf =builder.Configuration.AddJsonFile("appsettings.json").Build();

// Add services to the container.
   
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.AddAllActuators();
builder.WebHost.AddDiscoveryClient();
builder.Services.AddDbContext<ApplicationDbContext>(
        options => options.UseSqlServer(conf.GetConnectionString("Strconn"),
        b=>b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


builder.Services.AddTransient(typeof(IBaseRepository<>),typeof(BaseRepository <>));

builder.Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));

var app = builder.Build();



app.MapGet("/config/{name}", (string name)=> { return app.Configuration.GetValue<String>(name, "rr"); });

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

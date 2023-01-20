using CoreAPICrud.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ----------->>> Add UseInMemoryDatabase
// -------->>> START 
//builder.Services.AddDbContext<ContactAPIDbContext>(options => options.UseInMemoryDatabase("ContactsDb"));
// ----------->>> Add UseInMemoryDatabase
// -------->>> END 


// ----------->>> Add UseSQLServer
// -------->>> START 
builder.Services.AddDbContext<ContactAPIDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ContactDbCon")));
// ----------->>> Add UseSQLServer
// -------->>> END 



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

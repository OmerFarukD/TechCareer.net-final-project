using BookWebApi.Repository;
using BookWebApi.Service.Concrete;
using BookWebApi.Service.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Kitap yazar ve kategorilerle alakalý crud operasyonlarý
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IBookService,BookService>();
builder.Services.AddScoped<IAuthorService,AuthorService>();
builder.Services.AddScoped<ICategoryService,CategoryService>();


builder.Services.AddDbContext<BaseDbContext>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

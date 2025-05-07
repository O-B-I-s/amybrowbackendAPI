using amybrowbackendAPI.Data;
using amybrowbackendAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adding CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Adding services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IGalleryRepository, GalleryRepository>();
builder.Services.AddScoped<IDescriptionRepository, DescriptionRepository>();
builder.WebHost.UseWebRoot("wwwroot");
// pull in the connection string
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");

// adding DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connStr));
builder.Services.AddSwaggerGen();


var app = builder.Build();


// Use the CORS policy
app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseSwaggerUI(c =>

//    c.SwaggerEndpoint("/openapi/v1.json", "My API V1")

//);

app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.Run();

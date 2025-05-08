using amybrowbackendAPI.Data;
using amybrowbackendAPI.DTOs;
using amybrowbackendAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwt = builder.Configuration.GetSection("JwtSettings");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]))
        };
    });



var app = builder.Build();




// Seed default user
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var existingUser = await db.Users.FirstOrDefaultAsync(u => u.Email == "Amaratamaechi@gmail.com");
    if (existingUser == null)
    {
        var user = new User
        {
            Email = "Amaratamaechi@gmail.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Amaratamaechi@gmail.com")
        };

        db.Users.Add(user);
        await db.SaveChangesAsync();

        Console.WriteLine("Default admin user created.");
    }
}






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
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.Run();

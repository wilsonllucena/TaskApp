using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TaskApp.Data;
using TaskApp.Interfaces;
using TaskApp.Repositories;

var builder = WebApplication.CreateBuilder(args);
var secretKeyApi = "a7055d95-2581-4faa-b848-90815198c661";
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(); // Deve adcionar esse builder para carregar os controllers 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sistema de tarefas - API", Version = "v1"});

    var securitySchema = new OpenApiSecurityScheme()
    {
        Name = "JWT Autheticação",
        Description = "Entre com o JWT Token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference()
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            securitySchema, new string[] {}
        }
    });
});

// Configuração de banco de dados 
var connectionString = builder.Configuration["dbContextSettings:ConnectionString"];  
builder.Services.AddDbContext<AppDbContext>(options =>  
    options.UseNpgsql(connectionString)  
);

// Configuração de injeção de dependencias 
builder.Services.AddScoped<IUserRepository, UserRepository>(); 
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

//Configuração JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "your_business",
        ValidAudience = "your_application",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyApi))
    };
});
// Final configuração JWT

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Confifurar autheticação sempre UseAuthentication vem antes de  UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

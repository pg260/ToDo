using System.Text;
using AutoMapper;
using DefaultNamespace;
using Manager.API.ViewModels.TasksViewModel;
using Manager.API.ViewModels.UserViewModels;
using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Manager.Infra.Repositories;
using Manager.Services.AuthSettings;
using Manager.Services.DTO.Tasks;
using Manager.Services.DTO.User;
using Manager.Services.Interfaces;
using Manager.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Task = Manager.Domain.Entities.Task;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

// Add services to the container.

builder.Services.AddScoped<IAuthService, AuthServices>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();


builder.Services.AddScoped<IUserService, UserServices>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITaskService, TasksServices>();
builder.Services.AddScoped<ITaskRepository, Tasksepository>();

AutoMapperDependenceInjection();

void AutoMapperDependenceInjection()
{
    var autoMapperConfig = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<User, UserDTO>().ReverseMap();
        cfg.CreateMap<User, CreateUserDto>().ReverseMap();
        cfg.CreateMap<User, UpdateUserDto>().ReverseMap();
        cfg.CreateMap<CreateUserViewModel, CreateUserDto>().ReverseMap();
        cfg.CreateMap<UpdateViewModel, UpdateUserDto>().ReverseMap();

        cfg.CreateMap<Task, TasksDTO>().ReverseMap();
        cfg.CreateMap<Task, CreateTaskDto>().ReverseMap();
        cfg.CreateMap<Task, RemoveTaskDto>().ReverseMap();
        cfg.CreateMap<CreateTaskViewModel, CreateTaskDto>().ReverseMap();
        cfg.CreateMap<UpdateTaskViewModel, TasksDTO>().ReverseMap();
        cfg.CreateMap<DeleteTaskViewModel, RemoveTaskDto>().ReverseMap();
        cfg.CreateMap<SearchViewModel, SearchTask>().ReverseMap();
    });
    
    builder.Services.AddSingleton(autoMapperConfig.CreateMapper());
}

builder.Services.AddDbContext<ManagerContext>(options =>
    options.UseMySql("server=localhost;user id=informatica;password=Lab@inf019;database=ToDo",
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"), mySqlOptions => mySqlOptions.EnableRetryOnFailure()));

var key = Encoding.ASCII.GetBytes(Settings.Secret);

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo", Version = "v1", Description = "Desenvolvido por pg"});
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Autorização",
        Description = "Digite somente o token de acesso.",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

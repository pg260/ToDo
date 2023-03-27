using AutoMapper;
using Manager.API.ViewModels;
using Manager.API.ViewModels.TasksViewModel;
using Manager.API.ViewModels.UserViewModels;
using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Manager.Infra.Repositories;
using Manager.Services.DTO.Tasks;
using Manager.Services.DTO.User;
using Manager.Services.Interfaces;
using Manager.Services.Services;
using Microsoft.EntityFrameworkCore;
using Task = Manager.Domain.Entities.Task;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
    });
    
    builder.Services.AddSingleton(autoMapperConfig.CreateMapper());
}

builder.Services.AddDbContext<ManagerContext>(options =>
    options.UseMySql("server=localhost;user id=informatica;password=Lab@inf019;database=ToDo",
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql")));

builder.Services.AddControllers();
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

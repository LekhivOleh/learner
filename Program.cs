using System.Text.Json.Serialization;
using learner;
using learner.API.Interfaces.Repositories;
using learner.API.Interfaces.Services;
using learner.API.Repositories;
using learner.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext configuration
builder.Services.AddDbContext<LearnerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OrderActionsBy(apiDesc => apiDesc.HttpMethod);
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Register services and repositories
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IEntryRepository, EntryRepository>();
builder.Services.AddScoped<IEntryService, EntryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.MapGet("/", () => "Wassup");

app.Run();

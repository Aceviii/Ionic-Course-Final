using task_api.Migrations;
using task_api.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSqlite<TaskDbContext>("Data Source=task_api.db");
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
   options.AddPolicy("AllowOrigin", builder => builder
       .WithOrigins("http://localhost:8100", "http://localhost:8101") 
       .AllowAnyHeader()
       .AllowAnyMethod());
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowOrigin"); 
app.UseAuthorization();
app.MapControllers();

app.Run();

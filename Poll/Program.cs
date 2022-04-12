using Poll;
using Poll.Interfaces;
using Poll.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();


builder.Services.AddSingleton<Main>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPollService, PollService>();
builder.Services.AddTransient<IQuestionService, QuestionService>();
builder.Services.AddTransient<IPollQuestionService, PollQuestionService>();
builder.Services.AddTransient<IPollResponseService, PollResponseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();

app.Run();

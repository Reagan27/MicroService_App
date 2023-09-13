using JituPost.Data;
using JituPost.Extensions;
using JituPost.Services;
using JituPost.Service.IService;
using Microsoft.EntityFrameworkCore;
using JituPost.Services.IService;
using Cart.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connection to DB

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});
//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient("Comments", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrl:CommentsApi"])).AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();

//Services
builder.Services.AddScoped<IPostServices, PostService>();
builder.Services.AddScoped<ICommentInterface, CommentsService>();
builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();

//custom builders

builder.AddSwaggenGenExtension();
builder.AddAppAuthentication();
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

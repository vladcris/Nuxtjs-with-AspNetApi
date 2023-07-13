using Microsoft.EntityFrameworkCore;
using TrickingLibrary.Api;
using TrickingLibrary.Data;

var builder = WebApplication.CreateBuilder(args);

const string allCors = "nuxt-dev";

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("Dev"));

builder.Services.AddCors(opt => opt.AddPolicy(allCors, policy => {   
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));



var app = builder.Build();

if (app.Environment.IsDevelopment()) 
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(allCors);

app.UseStaticFiles();

app.MapControllers();

app.Run();
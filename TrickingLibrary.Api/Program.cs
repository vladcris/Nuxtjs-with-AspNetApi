using TrickingLibrary.Api;

var builder = WebApplication.CreateBuilder(args);

const string allCors = "nuxt-dev";

builder.Services.AddControllers();

builder.Services.AddSingleton<TrickStore>();

builder.Services.AddCors(opt => opt.AddPolicy(allCors, policy => {   
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(allCors);

app.MapControllers();

app.Run();
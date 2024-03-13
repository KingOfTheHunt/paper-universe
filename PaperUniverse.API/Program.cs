using PaperUniverse.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDatabase();
builder.AddAccountContext();
builder.AddMediatR();

var app = builder.Build();

app.MapAccountContextEndpoints();

app.Run();

using PaperUniverse.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDatabase();
builder.AddAccountContext();
builder.AddJwtAuthentication();
builder.AddMediatR();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapAccountContextEndpoints();

app.Run();

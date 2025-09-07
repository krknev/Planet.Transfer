using Planet.Transfer.Api.Application;
using Planet.Transfer.Api.Web;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddApplicationConfiguration(builder.Configuration)
    .AddApiComponents(builder.Configuration)
    ;

var app = builder.Build();

//app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.AddApiDevelopmentDocumentaion();
    app.UseDeveloperExceptionPage();
}
app.AddApiBuildConfiguration();

app.Run();


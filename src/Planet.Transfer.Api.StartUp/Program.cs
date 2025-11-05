using Planet.Transfer.Api.Application;
using Planet.Transfer.Api.Infrastructure;
using Planet.Transfer.Api.Web;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ConfigureHttpsDefaults(httpsOptions =>
//    {
//        var sslPath = builder.Configuration["SSL:Path"];
//        var sslPassword = builder.Configuration["SSL:Password"];
//        httpsOptions.ServerCertificate = X509CertificateLoader.LoadPkcs12FromFile(sslPath, sslPassword);
//    });
//});
builder.Services
    .AddApplicationConfiguration(builder.Configuration)
    .AddInfrastructureConfiguration(builder.Configuration)
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


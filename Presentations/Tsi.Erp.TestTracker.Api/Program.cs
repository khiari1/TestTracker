using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.FileProviders;
using Microsoft.Identity.Web;
using Serilog;
using Tsi.AspNetCore.Identity.AzureAD;
using Tsi.AutomatedTestRunner;
using Tsi.Erp.TestTracker.Api.Extentions;
using Tsi.Erp.TestTracker.Api.Services;
using Tsi.Erp.TestTracker.Core;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Erp.TestTracker.EntityFrameworkCore.DependencyInjection;
using Tsi.Erp.TestTracker.Hangfire;
using Tsi.Erp.TestTracker.TiketingSystem.AzureDevOps;
using Tsi.Extensions.Identity.Core;
using Tsi.Extensions.Identity.Stores;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var connectionString = configuration.GetConnectionString("DefaultStringConnection");

var azuredevOpsSection = configuration.GetSection("AzureDevOps");

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(configuration.GetSection("AzureAd"))
        .EnableTokenAcquisitionToCallDownstreamApi()
            .AddMicrosoftGraph(configuration.GetSection("MicrosoftGraph"))
            .AddInMemoryTokenCaches();




builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(opt =>
{
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

builder.Services.AddAuthorization()
    .AddHttpContextAccessor();

builder.Services
    .AddDataAccess(connectionString) // ef core
    .AddUserAzureAD()
    .AddService(typeof(ApplicationUser), typeof(TsiIdentityPermission))
    .AddJobSystem(connectionString) // hangfire system
    .AddCore()
    .AddMailService(configuration.GetSection<MailServiceOption>());// core of application;

//builder.Services.AddSingleton(sp =>
//            ChromeDriverService.CreateDefaultService("ChromeDriver"));

builder.Services.AddScoped<TestRunnerManager>();

builder.Services.AddScoped<Tsi.Erp.TestTracker.Api.Services.ITestRunnerService, TestRunnerService>();

builder.Services.AddSignalR();

builder.Services.AddScoped<IPushNotification, PushNotificationService>();

builder.Services.AddAuthorization(options =>
{
    foreach (Permissions permission in Enum.GetValues(typeof(Permissions)))
    {
        options.AddPolicy(permission.ToString(), policy =>
            policy.Requirements.Add(new PermissionRequirement(permission)));
    }
});

builder.Services.AddAutoMapper(typeof(Tsi.Erp.TestTracker.Api.AutoMapper.MappingConfiguration));

builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

builder.Services.AddRazorPages();

builder.Services.AddScoped<IHtmlRendrer, HtmlRendrer>();

builder.Services.AddScoped<PermissionAuthorizationHandler>();

builder.Services.AddTicketingSystem(azuredevOpsSection["OrganizationUrl"], azuredevOpsSection["PersonalAccessToken"]);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "default",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials();
                      });
});
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 100_000_000;
});
builder.Services.AddDirectoryBrowser();

#region Swagger region
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CompaQ",
        Version = "v1"
    });

    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = JwtBearerDefaults.AuthenticationScheme,
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddSwaggerGen();
#endregion

// Configuration de Serilog
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles()
   .UseStaticFiles()
   .UseFileServer(new FileServerOptions
   {
        FileProvider = new PhysicalFileProvider(builder.Environment.WebRootPath),
        RequestPath = "",
        EnableDirectoryBrowsing = true
   }); 

app.UseHangfireDashboard()
   .UseHttpsRedirection()
   .UseRouting()
   .UseCors("default")
   .UseAuthentication()
   .UseAuthorization();

app.MapHub<PushNotificationHub>("api/PushNotification");
app.MapControllers();
app.MapRazorPages();

app.Run();

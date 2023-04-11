using NLog;
using NLog.Web;
using Wiwi.GroupRobot.Api.Domain;
using Wiwi.GroupRobot.Common.Extensions;
using Wiwi.GroupRobot.Common.Extensions.SqlsugarExtenstion;
using Wiwi.GroupRobot.Common.Filters;
using Wiwi.GroupRobot.Common.Helper;

var builder = WebApplication.CreateBuilder(args);

#region log

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

#endregion

//配置
Appsettings.Configuration = builder.Configuration;

// Add services to the container.
builder.Services
    .AddAuthorization();
builder.Services.AddSqlSugarSetup();
builder.Services.AddCors(options => options.AddPolicy("AllowCors",
               builder =>
               {
                   builder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .SetIsOriginAllowed(_ => true)
                       .AllowCredentials();
               }));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerSetup();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver =
        new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(); //json字符串大小写原样输出
});

builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = false;
}).UseAutofac("Wiwi.GroupRobot.Api.dll");

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Host.UseNLog();

var app = builder.Build();

app.Services.SetServiceProvider();
app.UseCodeFirst(typeof(GroupRobotModel).Assembly);
app.UseHttpsRedirection();
app.UseSwaggerMiddleware();
//Cors
app.UseCors("AllowCors");

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
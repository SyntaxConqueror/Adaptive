using LR7.Services;
using LR7.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Encodings.Web;
using System.Text.Json;
using LR7.Database;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks().AddCheck<TimeHealthCheck>("time_check", null, new[] { "time" })
        .AddCheck<FileSystemHealthCheck>("file_system_check", null, new[] { "files" })
        .AddDbContextCheck<ApplicationDbContext>("database_health_check");
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

builder.Services.AddSingleton<string>("C:\\Users\\artsh\\OneDrive\\–абочий стол\\Work\\artishok\\app\\Console\\Kerne.php");
builder.Services.AddSingleton<FileSystemHealthCheck>();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new ApiVersion(3, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();

});

builder.Services.AddScoped<IUserService, UserService>(); //  ќдин екземпл€р серв≥су буде створений дл€ кожного HTTP-запиту
                                                         //  ÷е означаЇ, що вс≥ запити, €к≥ оброблюютьс€ в рамках одного HTTP-запиту, будуть використовувати той самий екземпл€р серв≥су

builder.Services.AddScoped<IVersioningService, VersioningService>();

builder.Services.AddSingleton<IPostService, PostService>(); // ќдин екземпл€р серв≥су буде створений на всю тривал≥сть роботи додатку
                                                            // ÷е означаЇ, що кожний HTTP-запит буде використовувати той самий екземпл€р серв≥су

builder.Services.AddTransient<ICommentService, CommentService>(); // ƒл€ кожного виклику серв≥су буде створений новий екземпл€р
                                                                  // ÷е означаЇ, що кожен раз, коли викликаЇтьс€ серв≥с, буде отримано новий екземпл€р

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "AppIssuer",
                ValidAudience = "AppAudience",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key_here_some_of_them_should_Be_longer"))
            };
        });

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

   
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseSwaggerUI();
    app.UseRouting();
#pragma warning disable ASP0014
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHealthChecks("/health/time", new HealthCheckOptions
        {
            Predicate = (check) => check.Tags.Contains("time"),
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";

                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };

                var result = new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(x => new
                    {
                        name = x.Key,
                        status = x.Value.Status.ToString(),
                        description = x.Value.Description
                    }),
                    totalDuration = report.TotalDuration
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(result, options));
            }
        });

        endpoints.MapHealthChecks("/health/files", new HealthCheckOptions
        {
            Predicate = (check) => check.Tags.Contains("files"),
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";

                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };

                var result = new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(x => new
                    {
                        name = x.Key,
                        status = x.Value.Status.ToString(),
                        description = x.Value.Description
                    }),
                    totalDuration = report.TotalDuration
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(result, options));
            }
        });

        endpoints.MapHealthChecks("/health/db");

        endpoints.MapHealthChecksUI(options =>
        {
            options.UIPath = "/hc-ui";
        });
    });
#pragma warning restore ASP0014
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

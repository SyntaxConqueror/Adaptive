using LR7.Services;
using LR7.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>(); //  ���� ��������� ������ ���� ��������� ��� ������� HTTP-������
                                                         //  �� ������, �� �� ������, �� ������������ � ������ ������ HTTP-������, ������ ��������������� ��� ����� ��������� ������

builder.Services.AddSingleton<IPostService, PostService>(); // ���� ��������� ������ ���� ��������� �� ��� ��������� ������ �������
                                                            // �� ������, �� ������ HTTP-����� ���� ��������������� ��� ����� ��������� ������

builder.Services.AddTransient<ICommentService, CommentService>(); // ��� ������� ������� ������ ���� ��������� ����� ���������
                                                                  // �� ������, �� ����� ���, ���� ����������� �����, ���� �������� ����� ���������

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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

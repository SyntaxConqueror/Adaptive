using LR7.Services;
using LR7.Services.Interfaces;

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

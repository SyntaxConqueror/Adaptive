using LR7.Services;
using LR7.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>(); //  ќдин екземпл€р серв≥су буде створений дл€ кожного HTTP-запиту
                                                         //  ÷е означаЇ, що вс≥ запити, €к≥ оброблюютьс€ в рамках одного HTTP-запиту, будуть використовувати той самий екземпл€р серв≥су

builder.Services.AddSingleton<IPostService, PostService>(); // ќдин екземпл€р серв≥су буде створений на всю тривал≥сть роботи додатку
                                                            // ÷е означаЇ, що кожний HTTP-запит буде використовувати той самий екземпл€р серв≥су

builder.Services.AddTransient<ICommentService, CommentService>(); // ƒл€ кожного виклику серв≥су буде створений новий екземпл€р
                                                                  // ÷е означаЇ, що кожен раз, коли викликаЇтьс€ серв≥с, буде отримано новий екземпл€р

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

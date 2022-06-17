using Chapter1.Contexts;
using Chapter1.Interfaces;
using Chapter1.Repositories;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors( options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyHeader();
    });
});

//padrão do token

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBerer";
    options.DefaultAuthenticateScheme = "JwtBerer";
}).AddJwtBearer("JwtBerer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter-chave-autenticacao")),
        ClockSkew = TimeSpan.FromMinutes(60),
        ValidIssuer = "chapter.webapi",
        ValidAudience = "chapter.webapi"
    };
});

builder.Services.AddScoped<ChapterContext, ChapterContext>();

builder.Services.AddTransient<LivroRepository, LivroRepository>();

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

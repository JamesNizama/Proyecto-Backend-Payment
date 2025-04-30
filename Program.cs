using MiWebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<EmpresaData>();
builder.Services.AddSingleton<VistaActividadUsuarioData>();
builder.Services.AddSingleton<VistaActividadPendienteData>();
builder.Services.AddSingleton<ActividadData>();
builder.Services.AddSingleton<UsuarioData>();
builder.Services.AddSingleton<AreaData>();
builder.Services.AddSingleton<DocumentoData>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {   
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();

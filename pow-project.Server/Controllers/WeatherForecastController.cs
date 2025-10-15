using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new() { Title = "pow-project API", Version = "v1" });

    var xml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xml);
    if (File.Exists(xmlPath))
        o.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

app.UseHttpsRedirection();
app.UseAuthorization();

// 👇 Swagger UI (dejalo ANTES del fallback)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "pow-project API v1");
    // Si querés verlo en la raíz:
    // c.RoutePrefix = string.Empty;
});

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();

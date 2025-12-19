using Abstracciones.Interfaces.Reglas;
using Reglas;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// =============================
// LEER CONFIGURACIÓN
// =============================
builder.Services.AddRazorPages();

// Pasar IConfiguration a Configuracion
builder.Services.AddScoped<IConfiguracion>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new Configuracion(configuration);
});

// =============================
// INICIAR API COMO PROCESO HIJO
// =============================
var apiPath = Path.Combine(AppContext.BaseDirectory, "..", "API", "API.exe");
Process? apiProcess = null;

if (File.Exists(apiPath))
{
    apiProcess = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = apiPath,
            UseShellExecute = false,
            CreateNoWindow = true, // No mostrar consola
            WorkingDirectory = Path.GetDirectoryName(apiPath)! // Muy importante
        }
    };

    apiProcess.Start();

    // Esperar un poco para que la API levante
    await Task.Delay(2000);
}

// =============================
// CONSTRUIR APP
// =============================
var app = builder.Build();

// =============================
// APAGAR API AL CERRAR WEB
// =============================
app.Lifetime.ApplicationStopping.Register(() =>
{
    try
    {
        if (apiProcess != null && !apiProcess.HasExited)
        {
            apiProcess.Kill(true);
        }
    }
    catch
    {
        // Ignorar errores al cerrar
    }
});

// =============================
// PIPELINE HTTP
// =============================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Si quieres probar local sin HTTPS:
// app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// =============================
// LEVANTAR WEB
// =============================
app.Run();
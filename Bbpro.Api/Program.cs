using Bbpro.Infrastructure.Contexts;
using Bbpro.Api.Configurations;
using Bbpro.Service.Extentions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Bbpro.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);


// Configure services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BbproDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        o => o.MigrationsAssembly("Bbpro.Api")
    );
    options.EnableSensitiveDataLogging();
});

// Configure Serilog for logging
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger).AddConsole();

// Configure custom service configurations
builder.Services.AddServiceFunctionsConfiguration()
              .AddErrorFilter()
              .AddImageSizeMax()
              .AddServiceConfig()
              .AddSwaggerService(builder.Configuration);

// Configure JSON serialization
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

var app = builder.Build();

// Apply database migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BbproDbContext>();
    dbContext.Database.Migrate();
}

// Configure middleware and routing
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseStaticFiles();
app.UseMiddleware<BbproMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.MapGet("/stream", (ILogger<Program> logger) =>
{
    string rtspUrl = "rtsp://admin:Dushanbe_2024@192.168.1.4:554/Streaming/Channels/101";
    logger.LogInformation("RTSP URL requested.");
    return Results.Ok(new { StreamUrl = rtspUrl });
})
.WithName("GetCameraStream")
.WithOpenApi();

app.Run();

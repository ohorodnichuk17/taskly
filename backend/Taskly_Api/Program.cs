using Microsoft.Extensions.FileProviders;
using Taskly_Api;
using Taskly_Api.Common;
using Taskly_Api.SignalR.Hubs;
using Taskly_Application;
using Taskly_Infrastructure;
using Taskly_Infrastructure.Common.Seeder;

var builder = WebApplication.CreateBuilder(args);

builder.Host.SerilogConfiguration();


// Add services to the container.
builder.Services
    .AddPresentation(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration);



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "images")),
    RequestPath = "/images"
});

app.UseCors("AllowPolicy");


DataInitializer.InitializeData(app);
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<BoardHub>("/board");
app.MapHub<CardCommentsHub>("/card-comments");

app.Run();

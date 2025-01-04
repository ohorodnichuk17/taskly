// var builder = WebApplication.CreateBuilder(args);
//
// // Add services to the container
// // builder.Services.AddAutoMapper(typeof(Program));.
//
// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// app.UseHttpsRedirection();
//
// app.UseAuthorization();
//
// app.MapControllers();
//
// app.Run();

using Taskly_Api;
using Taskly_Api.Common;
using Taskly_Application;
using Taskly_Infrastructure;
using Taskly_Infrastructure.Common.Seeder;

var builder = WebApplication.CreateBuilder(args);

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

app.UseCors(options => options.SetIsOriginAllowed(origin => true)
    .AllowCredentials()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

DataInitializer.InitializeData(app);
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

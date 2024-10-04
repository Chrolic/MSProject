using Parking.Services;
using Parking.Services.Interfaces;
using Swashbuckle.AspNetCore.SwaggerUI;
using Polly;
using Notification;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Configuration
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Add services
builder.Services.Scan(selector => selector
        .FromAssemblyOf<Program>()
        .AddClasses(classes => classes.Where(x => x.Name.EndsWith("Service")))
        .AsImplementedInterfaces());

builder.Services.Scan(selector => selector
        .FromAssemblyOf<NotificationAnchor>()
        .AddClasses(classes => classes.Where(x => x.Name.EndsWith("Service")))
        .AsImplementedInterfaces());

// Add Data
builder.Services.Scan(selector => selector
        .FromAssemblyOf<Program>()
        .AddClasses(classes => classes.Where(x => x.Name.EndsWith("Database")))
        .AsImplementedInterfaces());


// Add httpclient with Polly
builder.Services.AddHttpClient<IMotorAPIService, MotorAPIService>()
    .AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(1000*Math.Pow(2, attempt))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(uiOptions =>
{
    uiOptions.DocumentTitle = "Parking API";
    uiOptions.EnableFilter();
    uiOptions.DefaultModelsExpandDepth(-1);
    uiOptions.DisplayRequestDuration();
    uiOptions.DocExpansion(DocExpansion.None);
    uiOptions.RoutePrefix = "";

    uiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Parking API V1");

    //uiOptions.ConfigObject.AdditionalItems.Add("tagsSorter", "alpha");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

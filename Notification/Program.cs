using EventStore;
using Notification;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Scan(selector => selector
        .FromAssemblyOf<NotificationAnchor>()
        .AddClasses(classes => classes.Where(x => x.Name.EndsWith("Service")))
        .AsImplementedInterfaces());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI(uiOptions =>
{
    uiOptions.DocumentTitle = "Notification API";
    uiOptions.EnableFilter();
    uiOptions.DefaultModelsExpandDepth(-1);
    uiOptions.DisplayRequestDuration();
    uiOptions.DocExpansion(DocExpansion.None);
    uiOptions.RoutePrefix = "";

    uiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Parking API V1");

    //uiOptions.ConfigObject.AdditionalItems.Add("tagsSorter", "alpha"););
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

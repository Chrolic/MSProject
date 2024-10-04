using Pizzaria.Services;
using Pizzaria.Services.Interfaces;
using Swashbuckle.AspNetCore.SwaggerUI;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Scan(selector => selector
        .FromAssemblyOf<Program>()
        .AddClasses(classes => classes.Where(x => x.Name.EndsWith("Service")))
        .AsImplementedInterfaces());

// Add httpclient with Polly
builder.Services.AddHttpClient<IPizzariaService, PizzariaService>()
    .AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(1000 * Math.Pow(2, attempt))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();
app.UseSwaggerUI(uiOptions =>
{
    uiOptions.DocumentTitle = "Pizzaria API";
    uiOptions.EnableFilter();
    uiOptions.DefaultModelsExpandDepth(-1);
    uiOptions.DisplayRequestDuration();
    uiOptions.DocExpansion(DocExpansion.None);
    uiOptions.RoutePrefix = "";

    uiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Pizzaria API V1");

    //uiOptions.ConfigObject.AdditionalItems.Add("tagsSorter", "alpha");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

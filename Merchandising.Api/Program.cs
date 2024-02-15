using Framework.Configuration;
using Merchandising.Api.Extensions;

var configuration = ConfigurationHelper.GetConfiguration();

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddApplicationServices(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.EnsureDbCreated();
app.Run();

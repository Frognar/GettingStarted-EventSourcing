using BeerSender.Domain;
using BeerSender.EventStore;
using BeerSender.Web.EventPublishing;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.RegisterDomain();
builder.Services.RegisterEventStore();

builder.Services.AddEndpointsApiExplorer();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Beer Sender API")
            .WithDownloadButton(true)
            .WithTheme(ScalarTheme.Moon)
            .WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.JQuery);
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.MapHub<EventHub>("event-hub");

app.Run();

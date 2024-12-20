using cosmoschat.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging.AzureAppServices;
using System.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders().AddConsole();
        var loggingBuilder = builder.Logging.AddAzureWebAppDiagnostics();
        
        builder.Services.Configure<AzureFileLoggerOptions>(builder.Configuration.GetSection("AzureLogging"));

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSingleton<ChatService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");


        //app.UseEndpoints(endpoints =>
        //{
        //    endpoints.MapBlazorHub();
        //    endpoints.MapFallbackToPage("/_Host");
        //    endpoints.MapHub<ChatHub>(ChatHub.HubUrl);
        //});

        app.Run();
    }
}
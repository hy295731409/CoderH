//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.Run();

using ASPNETCORE6.Worker;

public class Program
{
    public static async Task Main(string[] args)
    {
        await createHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder createHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .ConfigureLogging(logging =>
            {
                logging.AddConsole();
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<TestWorker>();
                services.AddControllers();

            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.Configure((context, app) =>
                {
                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        app.UseDeveloperExceptionPage();
                    }
                    else
                    {
                        app.UseExceptionHandler("/Home/Error");
                    }
                    app.UseStaticFiles();

                    app.UseRouting();

                    app.UseAuthorization();

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");
                    });
                });
            });

}

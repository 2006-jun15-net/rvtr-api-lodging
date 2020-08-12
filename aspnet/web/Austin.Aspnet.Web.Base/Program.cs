using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Austin.Aspnet.Web.Base
{
  public class Program
  {
    public static void Main()
    {
      var host = Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webHost =>
      {
        webHost.Configure(appBuilder =>
        {
          appBuilder.UseRouting();
          appBuilder.UseEndpoints(endpoints =>
          {
            endpoints.MapGet("/html", async request =>
            {
              await request.Response.WriteAsync("<h1>hello web from html!</h1>");
            });

            endpoints.MapGet("/dist", async request =>
            {
              await request.Response.SendFileAsync("dist/index.html");
            });
          });
        });
      });

      host.Build().Run();
    }
  }
}

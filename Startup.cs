using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Westwind.AspNetCore.Markdown;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_markdown
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMarkdown(config =>
      {
        config.AddMarkdownProcessingFolder("/docs/", "~/Views/Shared/__MarkdownPageTemplate.cshtml");
      });
      services.AddRouting(x => x.LowercaseUrls = true);
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
        .AddRazorRuntimeCompilation();
      
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseDeveloperExceptionPage();

      app.UseDefaultFiles(new DefaultFilesOptions()
      {
        DefaultFileNames = new List<string> {"index.md", "index.html", "Index.cshtml"}
      });

      app.UseMarkdown();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseCors();
      app.UseEndpoints(endpoints =>
        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"));
    }

    public static void Main(string[] args) =>
      WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build().Run();
  }
}
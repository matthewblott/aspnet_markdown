using Microsoft.AspNetCore.Mvc;

namespace aspnet_markdown.controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

  }
    
}

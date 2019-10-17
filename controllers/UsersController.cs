using aspnet_markdown.models;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_markdown.controllers
{
  public class UsersController : Controller
  {
    public IActionResult Index()
    {
      var userViewModel = new UserViewModel();
      
      return View(userViewModel);
    }

    [HttpPost]
    public IActionResult Create()
    {
      return RedirectToAction(nameof(Index));

    }
    
  }
  
}
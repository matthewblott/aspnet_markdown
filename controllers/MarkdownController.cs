using Microsoft.AspNetCore.Mvc;
using aspnet_markdown.models;

namespace aspnet_markdown.controllers
{
  public class MarkdownController : Controller
  {
    public IActionResult Parsing()
    {
      return View();
    }

    public IActionResult TagHelper()
    {
      var model = new MarkdownViewModel
      {
        MarkdownText = @"This is **Markdown Text** that was bound to a `PageModel.MarkdownText` property:
```html
<markdown markdown=""MarkdownText"" />
```
"
      };

      return View(model);
    }

  }
}
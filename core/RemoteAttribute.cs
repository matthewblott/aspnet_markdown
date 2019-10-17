namespace aspnet_markdown.core
{
  public class RemoteAttribute : Microsoft.AspNetCore.Mvc.RemoteAttribute
  {
    public string Controller { get; }
    public string Action { get; }
    
    public RemoteAttribute(string action, string controller, string area) : base(action, controller, area)
    {
      Action = action;
      Controller = controller;
    }

  }
  
}
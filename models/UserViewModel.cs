using System.ComponentModel.DataAnnotations;

namespace aspnet_markdown.models
{
  public class UserViewModel
  {
    [MaxLength(20)]
    [Required]
    [Editable(true)]
    public string Username { get; set; }
    
    [MaxLength(20)]
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [MaxLength(20)]
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }  
    
    [MaxLength(20)]
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "The passwords do not match")]
    [Display(Name = "Password Compare", Prompt = "Password Compare")]
    public string PasswordCompare { get; set; }
    
  }
  
}
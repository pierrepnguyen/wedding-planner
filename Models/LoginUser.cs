using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wedding.Models
{
  public class LoginUser
  {
    [NotMapped]

    [Required(ErrorMessage = "Email input is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password input is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }
}
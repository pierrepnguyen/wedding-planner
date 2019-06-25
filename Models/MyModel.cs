using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wedding.Models
{
  public class User
  {
    [Key]
    public int UserId { get; set; }

    [Display(Name = "First name:")]
    [Required(ErrorMessage = "First name is required")]
    public string fName { get; set; }

    [Display(Name="Last name:")]
    [Required(ErrorMessage="Last name is required")]
    public string lName { get; set; }

    [Display(Name="Email:")]
    [Required(ErrorMessage="Email field is required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Display(Name="Password:")]
    [Required]
    [MinLength(8, ErrorMessage="Password needs to be 8 characters or longer")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public List<RSVP> Weddings { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [NotMapped]
    [Compare("Password")]
    [Display(Name="Confirm Password:")]
    [DataType(DataType.Password, ErrorMessage="Passwords do not match")]
    public string Confirm {get; set;}
  }
}
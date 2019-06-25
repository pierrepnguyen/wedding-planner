using System;
using System.ComponentModel.DataAnnotations;

namespace wedding.Models
{
  public class Both
  {
    public User user { get; set; }
    [DataType(DataType.Password)]
    public LoginUser login { get; set; }
  }
}
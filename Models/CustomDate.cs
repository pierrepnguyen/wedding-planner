using System;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
  public class CustomDateAttribute : ValidationAttribute
  {
    public CustomDateAttribute()
    {
    }
    public override bool IsValid(object value)
    {
      var dt = (DateTime)value;
      if(dt >= DateTime.Now)
      {
        return true;
      }
      return false;
    }
  }
}
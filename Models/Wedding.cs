using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WeddingPlanner.Models;

namespace wedding.Models
{
  public class Wedding
  {
    [Key]
    public int WeddingId { get; set; }

    [Display(Name="Wedder One:")]
    [Required(ErrorMessage="This input is required.")]
    public string WedderOne { get; set; }

    [Display(Name="Wedder Two:")]
    [Required(ErrorMessage="This input is required.")]
    public string WedderTwo { get; set; }

    [Display(Name="Wedding Date:")]
    [Required(ErrorMessage="Date input is requied.")]
    [DataType(DataType.DateTime)]
    [CustomDate(ErrorMessage="Date must be in the future.")]
    public DateTime Date { get; set; }

    [Display(Name="Wedding Address:")]
    [Required(ErrorMessage="Address input is required.")]
    public string Address { get; set; }

    public int UserId {get;set;}
    public List<RSVP> Guests { get; set; }


    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

  }
}
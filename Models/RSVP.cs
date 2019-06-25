using System;

namespace wedding.Models
{
  public class RSVP
  {
    public int RSVPId { get; set; }
    public int UserId { get; set; }
    public int WeddingId { get; set; }
    public User User { get; set; }
    public Wedding Wedding { get; set; }
  }
}
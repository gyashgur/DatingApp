using System;

namespace DatingApp.API.Models
{
  public class Photo
  {
    public int Id { get; set; }

    public string Url { get; set; }

    public string Description { get; set; }

    public DateTime DateAdded { get; set; }

    public bool IsMain { get; set; }

    public User User { get; set; }   // To make in stead of the resticted delete cascading delete

    public int UserId { get; set; }  // To make in stead of the resticted delete cascading delete
  }
}
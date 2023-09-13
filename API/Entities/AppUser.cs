using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class AppUser
{
  [Key]
  public int Id { get; set; } //When named the prop is Id => The EF automatically consume that prop is Primary Key in DB 
  public string UserName { set; get; }
}

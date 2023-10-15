
using System.ComponentModel.DataAnnotations;
using API.Extensions;

namespace API.Entities
{
  public class AppUser
  {
    [Key]
    public int Id { get; set; } //When named the prop is Id => The EF automatically consume that prop is Primary Key in DB 
    public string UserName { set; get; }
    // API server is going to return a response as JSON and convention for JSON is to  use CAMEL case. EX: Id => id, UserName => userName
    public byte[] PasswordHash { set; get; }
    public byte[] PasswordSalt { get; set; }
    // When add new prop in DB => we need to create new migration 

    // DateOnly help us to only track the date of something Ex: Date of birth
    public DateOnly DateOfBirth { get; set; }
    public string KnowAs { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public string Gender { get; set; }
    public string Introduction { get; set; }
    public string LookingFor { get; set; }
    public string Interests { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public List<Photo> Photos { get; set; } = new();
    public int GetAge()
    {
      return DateOfBirth.CalculateAge();
    }
  }
}

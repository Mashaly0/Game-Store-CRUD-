namespace WebApplication1.Models
{
    public class Category : EntityBase
    {
      public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}

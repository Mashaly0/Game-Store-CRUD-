
namespace WebApplication1.Models
{
    public class Devices : EntityBase
    {
        [MaxLength(50)]
        public string Icon { get; set; } = string.Empty;
    }

}
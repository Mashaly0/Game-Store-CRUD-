namespace WebApplication1.Models
{
    public class GameDevices
    {
        public int GameId { get; set; }
        public Game Game { get; set; } = default;

        public int DevicesId { get; set; }

        public Devices Devices { get; set; } = default;

    }
}
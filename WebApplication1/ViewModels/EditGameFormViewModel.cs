using WebApplication1.Attributes;

namespace WebApplication1.ViewModels
{
    public class EditGameFormViewModel : GameFormViewModel
    {
        public int Id { get; set; }

        public string? CurrentCover { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtensions)]
        public IFormFile? Cover { get; set; } = default!;

    }
}

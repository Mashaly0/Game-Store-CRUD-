
using WebApplication1.Attributes;

namespace WebApplication1.ViewModels
{
    public class CreateGameFormViewModel : GameFormViewModel
    {
        [AllowedExtensions(FileSettings.AllowedExtensions)]
        public IFormFile Cover { get; set; } = default!;
    }

}

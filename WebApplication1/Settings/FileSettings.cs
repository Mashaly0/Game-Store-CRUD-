using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Settings
{
    public static class FileSettings
    {
        public const string ImagePath = "/assets/images/games";
        public const string AllowedExtensions  = ".jpg,.jpeg,.png";
        public const int MaxFileSizeInMb = 1;
        public const int MaxFileSizeInBytes = MaxFileSizeInMb * 1024 ;

    }
}

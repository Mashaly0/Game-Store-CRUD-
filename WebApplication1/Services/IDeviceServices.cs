namespace WebApplication1.Services
{
    public interface IDeviceService
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}

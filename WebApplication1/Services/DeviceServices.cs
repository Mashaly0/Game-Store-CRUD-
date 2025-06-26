
namespace WebApplication1.Services
{
    public class DeviceServices : IDeviceService
    {
        private readonly ApplicationDbcontext _Context;


        public DeviceServices(ApplicationDbcontext context)
        {
            _Context = context;
        }

        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _Context.Devices
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text)
                .AsNoTracking()
                .ToList();

        }
    }
}

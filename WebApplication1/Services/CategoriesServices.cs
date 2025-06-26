
namespace WebApplication1.Services
{
    public class CategoriesServices : ICategoriesServices
    {
            
    private readonly ApplicationDbcontext _Context;

        public CategoriesServices(ApplicationDbcontext context)
        {
            _Context = context;
        }

        public IEnumerable<SelectListItem> GetSelectList()
        {
          return  _Context.Categories
               .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
               .OrderBy(c => c.Text)
               .AsNoTracking()
               .ToList();

        }
    }
}

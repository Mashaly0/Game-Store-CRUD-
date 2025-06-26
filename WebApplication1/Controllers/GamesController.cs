
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbcontext _Context;
        private readonly ICategoriesServices _categoriesServices;
        private readonly IDeviceService _deviceService;
        private readonly IGameServices _gameServices;


        public GamesController(ApplicationDbcontext context, ICategoriesServices categoriesServices,
            IDeviceService deviceService, IGameServices gameServices)
        {
            _categoriesServices = categoriesServices;
            _deviceService = deviceService;
            _Context = context;
            _gameServices = gameServices;
        }
        public IActionResult Index()
        {
            var games = _gameServices.GetAll();
            return View(games);
        }
        public IActionResult Details(int id)
        {
            var game = _gameServices.GetById(id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //var categories = _context.Categories.ToList(); // you will need to cast in this siat

            CreateGameFormViewModel ViewModel = new()
            {

                Devices = _deviceService.GetSelectList(),
                // change and data
                Categories = _categoriesServices.GetSelectList(),


            };
            return View(ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Devices = _deviceService.GetSelectList();
                model.Categories = _categoriesServices.GetSelectList();

                return View(model);
            }
            await _gameServices.Create(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gameServices.GetById(id);

            if (game is null)
                return NotFound();

            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Device.Select(d => d.DevicesId).ToList(),
                Categories = _categoriesServices.GetSelectList(),
                Devices = _deviceService.GetSelectList(),
                CurrentCover = game.Cover
            };

            return View(viewModel);
        }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(EditGameFormViewModel model)
            {
                if (!ModelState.IsValid)
                {
                    model.Categories = _categoriesServices.GetSelectList();
                    model.Devices = _deviceService.GetSelectList();
                    return View(model);
                }

                var game = await _gameServices.Update(model);

                if (game is null)
                    return BadRequest();

                return RedirectToAction(nameof(Index));
            }



        public IActionResult Delete(int id)
        {

            var isDeleted = _gameServices.Delete(id);

            if (isDeleted) return RedirectToAction(nameof(Index));

            return BadRequest();

        }
    }

    } 

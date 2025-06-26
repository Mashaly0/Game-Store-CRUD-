
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class GameServices : IGameServices
    {
        private readonly ApplicationDbcontext _Context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;
        public GameServices(ApplicationDbcontext context, IWebHostEnvironment webHostEnvironment)
        {
            _Context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        }

        public Game? GetById(int id)
        {
            return _Context.Games
           .Include(g => g.Category)
           .Include(g => g.Device)
           .ThenInclude(g => g.Devices)
           .AsNoTracking()
           .SingleOrDefault(g => g.Id == id);

        }

        public IEnumerable<Game> GetAll()
        {
            return _Context.Games
           .Include(g => g.Category)
           .Include(g => g.Device)
           .ThenInclude(g => g.Devices)
           .AsNoTracking()
           .ToList();

        }

        public async Task Create(CreateGameFormViewModel model)
        {
            var coverName = await SaveCover(model.Cover);
            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Device = model.SelectedDevices.Select(d => new GameDevices { DevicesId = d }).ToList()

            };
            _Context.Add(game);
            _Context.SaveChanges();

        }

        public async Task<Game?> Update(EditGameFormViewModel model)
        {
            var game = _Context.Games
                .Include(g => g.Device)
                .SingleOrDefault(g => g.Id == model.Id);

            if (game is null)
                return null;

            var hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;

            // Update main properties
            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;

            // 🔥 Remove existing device associations
            _Context.GameDevices.RemoveRange(game.Device);

            // ✅ Add new device associations
            game.Device = model.SelectedDevices.Select(d => new GameDevices { DevicesId = d, GameId = game.Id }).ToList();

            // ✅ Update the cover if a new one was uploaded
            if (hasNewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }

            var effectedRows = _Context.SaveChanges();

            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var oldCoverPath = Path.Combine(_imagesPath, oldCover);
                    if (File.Exists(oldCoverPath))
                        File.Delete(oldCoverPath);
                }

                return game;
            }
            else
            {
                if (hasNewCover)
                {
                    var newCoverPath = Path.Combine(_imagesPath, game.Cover);
                    if (File.Exists(newCoverPath))
                        File.Delete(newCoverPath);
                }

                return null;
            }
        }


        public bool Delete(int id)
        {
            var isDeleted = false;

            var game = _Context.Games.Find(id);

            if (game is null) return isDeleted;

            _Context.Remove(game);

            var effectedRows = _Context?.SaveChanges();
            if (effectedRows > 0)

            {
                isDeleted = true;

                var cover = Path.Combine(_imagesPath, game.Cover);

                File.Delete(cover);
               
            }


            return isDeleted;
        }

        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagesPath, coverName);
            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
            return coverName;

        }

    }
}
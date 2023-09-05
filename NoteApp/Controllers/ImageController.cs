using Microsoft.AspNetCore.Mvc;
using NoteApp.Data;
using NoteApp.Models;

namespace NoteApp.Controllers
{
    public class ImageController : Controller
    {
        private readonly UNContext _context;

        public ImageController(UNContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            var image = new Images
            {
                FileName = Guid.NewGuid().ToString(), // Dosya adını otomatik olarak oluşturun
                ContentType = file.ContentType
            };

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                image.Data = ms.ToArray();
            }

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return Ok("File uploaded successfully.");
        }
    }

}

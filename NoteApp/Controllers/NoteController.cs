using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteApp.Data;
using NoteApp.Models;
using System.Security.Claims;

namespace NoteApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly UNContext _context;

        public NoteController(UNContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult NoteView()
        {
            // Kullanıcının notlarını tarih sırasına göre tersten sıralı olarak al
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var notes = _context.Notes.Where(n => n.UserId == userId).OrderByDescending(note => note.CreatedAt).ToList();

            // Notları görüntülemek için View metoduna veriyi geçir
            return View(notes);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Note model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcının kimlik bilgisini al
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Notu oluştur ve kullanıcıya ilişkilendir
                model.UserId = userId;
                model.CreatedAt = DateTime.Now;

                if (file != null && file.Length > 0)
                {
                    // Eğer resim yüklendi ise, Image modelini oluştur
                    var image = new Images
                    {
                        Data = new byte[file.Length],
                        FileName = file.FileName,
                        ContentType = file.ContentType
                    };

                    // Resim verisini oku ve Image modeline kaydet
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        image.Data = stream.ToArray();
                    }

                    // Image modelini not ile ilişkilendir
                    model.Image = image;
                }

                // Notu ve resmi veritabanına kaydet
                _context.Notes.Add(model);
                _context.SaveChanges();

                return RedirectToAction("NoteView", "Note");
            }

            return View(model);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var note = _context.Notes.FirstOrDefault(n => n.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(Note model)
        {
            if (ModelState.IsValid)
            {
                var note = _context.Notes.FirstOrDefault(n => n.Id == model.Id);

                if (note == null)
                {
                    return NotFound();
                }

                // Notu güncelle
                note.Title = model.Title;
                note.Content = model.Content;
                _context.SaveChanges();

                return RedirectToAction("NoteView", "Note");
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var note = _context.Notes.FirstOrDefault(n => n.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            // Notu sil
            _context.Notes.Remove(note);
            _context.SaveChanges();

            return RedirectToAction("NoteView", "Note");
        }

        [Authorize]
        public IActionResult View(int id)
        {
            var note = _context.Notes.Include(n => n.Image).FirstOrDefault(n => n.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }


        [AllowAnonymous]
        public IActionResult GetImage(int id)
        {
            var image = _context.Images.FirstOrDefault(i => i.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return File(image.Data, image.ContentType);
        }
    }

}



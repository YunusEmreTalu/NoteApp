// Image.cs modeli
namespace NoteApp.Models
{
    public class Images
    {
        public int Id { get; set; }
        public byte[]? Data { get; set; } // Resim verisini byte dizisi olarak saklamak için
        public string? FileName { get; set; } // Resim dosya adı
        public string? ContentType { get; set; } // Resim içerik türü (örn. image/jpeg)

        // Notlarla ilişkilendirmek için NoteId özelliği
        public int NoteId { get; set; }
        public Note? Note { get; set; }
    }
}

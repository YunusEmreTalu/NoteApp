namespace NoteApp.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }

        // Kullanıcı ile ilişkilendirme için UserId özelliği
        public string? UserId { get; set; }
        public User? User { get; set; }

        // Resim ile ilişkilendirme için Image özelliği
        public Images? Image { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace NoteApp.Models
{
    public class User : IdentityUser
    {
        // User sınıfına özgü özellikler
        public override string? UserName { get; set; }
        public string? Password { get; set; }
        public override string? Email { get; set; }

        // Parola onayı için ConfirmPassword özelliği
        public string? ConfirmPassword { get; set; }

        // İlişkili notları tutmak için Note koleksiyonu
        public ICollection<Note>? Notes { get; set; }
    }
}

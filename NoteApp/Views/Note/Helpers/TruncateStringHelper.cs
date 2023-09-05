using System;

namespace NoteApp.Views.Note.Helpers
{
    public static class TruncateStringHelper
    {
        public static string TruncateString(string? input, int length)
        {
            if (string.IsNullOrEmpty(input) || input.Length <= length)
            {
                return input ?? ""; // Eğer input null ise, boş bir string döndür.
            }
            else
            {
                return input.Substring(0, length) + "...";
            }
        }
    }
}

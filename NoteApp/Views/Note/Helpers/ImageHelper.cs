namespace NoteApp.Views.Note.Helpers
{
    public static class ImageHelper
    {
        public static string Base64UrlEncode(byte[] data)
        {
            return Convert.ToBase64String(data).Replace('+', '-').Replace('/', '_').TrimEnd('=');
        }
    }
}
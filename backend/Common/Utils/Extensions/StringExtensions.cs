namespace POD.Common.Utils.Extensions
{
    public static class StringExtensions
    {
        public static string DiscardFileExtension(this string filename)
        {
            return Path.GetFileNameWithoutExtension(filename);
        }
    }
}
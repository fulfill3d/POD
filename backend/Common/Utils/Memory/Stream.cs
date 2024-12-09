using System.Text;

namespace POD.Common.Utils.Memory
{
    public static class Stream
    {
        public static System.IO.Stream TextToStream(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            return new MemoryStream(bytes);
        }

        public static async Task<string> StreamToText(System.IO.Stream contentStream)
        {
            return await new StreamReader(contentStream).ReadToEndAsync();
        }
    }
    
}
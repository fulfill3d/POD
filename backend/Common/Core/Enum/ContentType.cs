namespace POD.Common.Core.Enum
{
    public class ContentType
    {
                private readonly string _mimeType;

        private ContentType(string mimeType)
        {
            _mimeType = mimeType;
        }

        public override string ToString()
        {
            return _mimeType;
        }

        public static ContentType PLAIN => new ContentType("text/plain");
        public static ContentType HTML => new ContentType("text/html");
        public static ContentType CSV => new ContentType("text/csv");
        
        public static ContentType JPEG => new ContentType("image/jpeg");
        public static ContentType PNG => new ContentType("image/png");
        public static ContentType GIF => new ContentType("image/gif");
        public static ContentType SVG => new ContentType("image/svg+xml");
        public static ContentType TIFF => new ContentType("image/tiff");
        
        public static ContentType MPEG => new ContentType("audio/mpeg");
        public static ContentType OGG => new ContentType("audio/ogg");
        
        public static ContentType MP4 => new ContentType("video/mp4");
        public static ContentType VOGG => new ContentType("video/ogg");
        public static ContentType WEBM => new ContentType("video/webm");
        
        public static ContentType JSON => new ContentType("application/json");
        public static ContentType XML => new ContentType("application/xml");
        public static ContentType ZIP => new ContentType("application/zip");
        public static ContentType RAR => new ContentType("application/x-rar-compressed");
        public static ContentType TAR => new ContentType("application/x-tar");
        public static ContentType GZIP => new ContentType("application/gzip");
        public static ContentType BIN => new ContentType("application/octet-stream");
        public static ContentType EXE => new ContentType("application/x-msdownload");
        public static ContentType STL => new ContentType("model/stl");
        public static ContentType OBJ => new ContentType("model/obj");
    }
}
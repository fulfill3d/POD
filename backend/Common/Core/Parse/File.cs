using POD.Common.Core.Enum;

namespace POD.Common.Core.Parse
{
    public class File
    {
        public static ContentType GetContentType(string mimeType)
        {
            switch (mimeType.ToLower())
            {
                case ".stl":
                    return ContentType.STL;
                case ".obj":
                    return ContentType.OBJ;
                case ".jpeg":
                case ".jpg":
                    return ContentType.JPEG;
                case ".png":
                    return ContentType.PNG;
                case ".gif":
                    return ContentType.GIF;
                case ".svg":
                    return ContentType.SVG;
                case ".tiff":
                    return ContentType.TIFF;
                case ".mp3":
                    return ContentType.MPEG;
                case ".ogg":
                    return ContentType.OGG;
                case ".mp4":
                    return ContentType.MP4;
                case ".ogv":
                    return ContentType.VOGG;
                case ".webm":
                    return ContentType.WEBM;
                case ".json":
                    return ContentType.JSON;
                case ".xml":
                    return ContentType.XML;
                case ".zip":
                    return ContentType.ZIP;
                case ".rar":
                    return ContentType.RAR;
                case ".tar":
                    return ContentType.TAR;
                case ".gz":
                    return ContentType.GZIP;
                case ".bin":
                    return ContentType.BIN;
                case ".exe":
                    return ContentType.EXE;
                default:
                    return ContentType.BIN; // Default to a generic binary if no match is found
            }
        }

        public static string GetExtension(string contentType)
        {
            if (contentType == ContentType.JPEG.ToString())
                return ".jpeg";
            if (contentType == ContentType.PNG.ToString())
                return ".png";
            if (contentType == ContentType.GIF.ToString())
                return ".gif";
            if (contentType == ContentType.SVG.ToString())
                return ".svg";
            if (contentType == ContentType.TIFF.ToString())
                return ".tiff";
            if (contentType == ContentType.MPEG.ToString())
                return ".mp3";
            if (contentType == ContentType.OGG.ToString())
                return ".ogg";
            if (contentType == ContentType.MP4.ToString())
                return ".mp4";
            if (contentType == ContentType.VOGG.ToString())
                return ".ogv";
            if (contentType == ContentType.WEBM.ToString())
                return ".webm";
            if (contentType == ContentType.JSON.ToString())
                return ".json";
            if (contentType == ContentType.XML.ToString())
                return ".xml";
            if (contentType == ContentType.ZIP.ToString())
                return ".zip";
            if (contentType == ContentType.RAR.ToString())
                return ".rar";
            if (contentType == ContentType.TAR.ToString())
                return ".tar";
            if (contentType == ContentType.GZIP.ToString())
                return ".gz";
            if (contentType == ContentType.BIN.ToString())
                return ".bin";
            if (contentType == ContentType.EXE.ToString())
                return ".exe";
            if (contentType == ContentType.STL.ToString())
                return ".stl";
            if (contentType == ContentType.OBJ.ToString())
                return ".obj";
            return string.Empty; // Or handle unknown type appropriately
        }
        
    }
}
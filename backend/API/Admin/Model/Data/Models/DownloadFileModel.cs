using POD.Common.Core.Enum;

namespace POD.API.Admin.Model.Data.Models
{
    public class DownloadFileModel
    {
        private string _type;

        public Stream Content { get; set; }
        public string Name { get; set; }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private ContentType _contentType;
        public ContentType ContentType
        {
            get { return _contentType; }
            set
            {
                _contentType = value;
                Type = _contentType.ToString();
            }
        }
        
    }
}
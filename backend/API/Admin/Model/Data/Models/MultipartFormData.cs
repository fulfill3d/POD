namespace POD.API.Admin.Model.Data.Models
{
    public class MultipartFormData
    {
        public List<FormFile> Files { get; } = new List<FormFile>();
        public List<FormField> Fields { get; } = new List<FormField>();
    }
    
    public class FormFile
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }

    public class FormField
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
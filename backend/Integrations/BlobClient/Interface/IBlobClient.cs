using POD.Integrations.BlobClient.Model;

namespace POD.Integrations.BlobClient.Interface
{
    public interface IBlobClient
    {
        Task<string> Upload(Blob blob);
        Task<Stream> Download(Blob blob);
        Task Delete(Blob blob);
    }
}
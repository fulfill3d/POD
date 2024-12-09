using POD.API.Admin.Filament.Data.Models;

namespace POD.API.Admin.Filament.Services.Interfaces
{
    public interface IFilamentService
    {
        Task<string> GetAllFilaments();
        Task<string> GetFilament(int id);
        Task<bool> AddNewFilament(AddFilamentRequest request);
        Task<bool> UpdateFilamentStock(UpdateFilamentStockRequest request);
        Task<bool> DeleteFilament(int filamentId);
    }
}
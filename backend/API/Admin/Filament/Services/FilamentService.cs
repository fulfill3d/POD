using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using POD.API.Admin.Filament.Data.Database;
using POD.API.Admin.Filament.Data.Models;
using POD.API.Admin.Filament.Services.Interfaces;
using POD.Common.Database.Models;

namespace POD.API.Admin.Filament.Services
{
    public class FilamentService(ILogger<FilamentService> logger, FilamentContext dbContext) : IFilamentService
    {
        public async Task<string> GetAllFilaments()
        {
            var filaments = await dbContext.Filaments
                .Include(f => f.Material)
                .Include(f => f.Brand)
                .Include(f => f.Color)
                .Where(f => f.IsActive == true)
                .Select(f => new POD.Common.Database.Models.Filament
                {
                    Id = f.Id,
                    Name = f.Name,
                    ColorId = f.ColorId,
                    MaterialId = f.MaterialId,
                    BrandId = f.BrandId,
                    Description = f.Description,
                    Cost = f.Cost,
                    StockQuantity = f.StockQuantity,
                    CreatedAt = f.CreatedAt,
                    UpdatedAt = f.UpdatedAt,
                    SpoolWeight = f.SpoolWeight,
                    SpoolWeightUnitId = f.SpoolWeightUnitId,
                    Material = f.Material,
                    Brand = f.Brand,
                    Color = f.Color,
                }).ToListAsync();

            return JsonConvert.SerializeObject(filaments, Formatting.Indented);
        }

        public async Task<string> GetFilament(int id)
        {
            var filament = await dbContext.Filaments
                .Include(f => f.Material)
                .ThenInclude(fm => fm.GeneralMaterial)
                .Include(f => f.Brand)
                .Include(f => f.Color)
                .Where(f => f.IsActive == true && f.Id == id)
                .Select(f => new POD.Common.Database.Models.Filament
                {
                    Id = f.Id,
                    Name = f.Name,
                    ColorId = f.ColorId,
                    MaterialId = f.MaterialId,
                    BrandId = f.BrandId,
                    Description = f.Description,
                    Cost = f.Cost,
                    StockQuantity = f.StockQuantity,
                    CreatedAt = f.CreatedAt,
                    UpdatedAt = f.UpdatedAt,
                    SpoolWeight = f.SpoolWeight,
                    SpoolWeightUnitId = f.SpoolWeightUnitId,
                    Material = new FilamentMaterial
                    {
                        Id = f.Material.Id,
                        GeneralMaterialId = f.Material.GeneralMaterialId,
                        Name = f.Material.Name,
                        Description = f.Material.Description,
                        IsActive = f.Material.IsActive,
                        Density = f.Material.Density,
                        DensityUnitId = f.Material.DensityUnitId,
                        GeneralMaterial = f.Material.GeneralMaterial
                    },
                    Brand = f.Brand,
                    Color = f.Color,
                }).FirstOrDefaultAsync();
            
            if (filament == null) return string.Empty;

            return JsonConvert.SerializeObject(filament, Formatting.Indented);
        }

        public async Task<bool> AddNewFilament(AddFilamentRequest request)
        {
            try
            {
                var filament = new Common.Database.Models.Filament
                {
                    Name = request.Name,
                    ColorId = request.ColorId,
                    MaterialId = request.MaterialId,
                    BrandId = request.BrandId,
                    Description = request.Description,
                    Cost = request.Cost,
                    StockQuantity = request.StockQuantity,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsActive = true,
                    SpoolWeight = request.SpoolWeight,
                    SpoolWeightUnitId = request.SpoolWeightUnitId,
                };

                await dbContext.Filaments.AddAsync(filament);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateFilamentStock(UpdateFilamentStockRequest request)
        {
            try
            {
                var filament = await dbContext.Filaments
                    .Where(f => f.Id == request.FilamentId)
                    .FirstOrDefaultAsync();
                
                if (filament == null)
                {
                    return false;
                }

                if (filament.StockQuantity > request.StockQuantity)
                {
                    filament.StockQuantity = request.StockQuantity;
                    
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                
                var newFilamentAmount = request.StockQuantity - filament.StockQuantity;
                var oldFilamentTotalCost = filament.StockQuantity * filament.SpoolWeight * filament.Cost;
                var newFilamentTotalCost = request.Cost * newFilamentAmount;
                var totalCost = newFilamentTotalCost + oldFilamentTotalCost;
                var newCost = totalCost / request.StockQuantity;
                    
                //Update DB
                filament.StockQuantity = request.StockQuantity;
                filament.Cost = newCost;
                    
                await dbContext.SaveChangesAsync();
                return true;
                
            }
            
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteFilament(int filamentId)
        {
            try
            {
                var filament = await dbContext.Filaments
                    .Where(f => f.Id == filamentId)
                    .FirstOrDefaultAsync();
                
                if (filament == null)
                {
                    return false;
                }
                
                filament.IsActive = false;

                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
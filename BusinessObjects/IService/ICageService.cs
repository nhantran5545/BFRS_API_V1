using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface ICageService
    {
        Task CreateCageAsync(Cage cage);
        void UpdateCage(Cage cage);
        void DeleteCage(Cage cage);
        void DeleteCageById(object cageId);
        Task<IEnumerable<Cage>> GetAllCagesAsync();
        Task<Cage?> GetCageByIdAsync(object cageId);
    }
}

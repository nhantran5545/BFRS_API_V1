using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IBreedingService
    {
        Task<float> CalculateInbreedingPercentage(Bird FatherBird, Bird MotherBird);
        Task CreateBreeding(Breeding breeding);
        void UpdateBreeding(Breeding breeding);
        void DeleteBreeding(Breeding breeding);
        void DeleteBreedingById(object breedingId);
        Task<IEnumerable<Breeding>> GetAllBreedings();
        Task<IEnumerable<Breeding>> GetAllBreedingsByManagerId(object managerId);
        Task<Breeding?> GetBreedingById(object breedingId);
    }
}

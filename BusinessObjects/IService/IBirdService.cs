using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IBirdService
    {
        Task<int> CreateBirdAsync(Bird bird);
        void UpdateBird(Bird bird);
        void DeleteBird(Bird bird); 
        void DeleteBirdById(object birdId);
        Task<IEnumerable<Bird>> GetAllBirdsAsync();
        Task<IEnumerable<Bird>> GetAllBirdsByFarmId(object farmId);
        Task<Bird?> GetBirdByIdAsync(object birdId);
    }
}

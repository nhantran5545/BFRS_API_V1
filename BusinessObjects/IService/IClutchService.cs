using BusinessObjects.ResponseModels;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IClutchService
    {
        Task CreateClutchAsync(Clutch clutch);
        void UpdateClutch(Clutch clutch);
        void DeleteClutch(Clutch clutch);
        void DeleteClutchtById(object clutchId);
        Task<IEnumerable<ClutchResponse>> GetAllClutchsAsync();
        Task<IEnumerable<ClutchResponse>> GetAllClutchsByBreedingId(object breedingId);
        Task<IEnumerable<ClutchResponse>> GetAllClutchsByCreatedById(object CreatedById);
        Task<ClutchDetailResponse?> GetClutchByIdAsync(object clutchId);
    }
}

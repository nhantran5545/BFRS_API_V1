using BusinessObjects.RequestModels;
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
        Task<int> CreateClutchAsync(ClutchAddRequest clutchAddRequest);
        Task<bool> CloseClutch(ClutchCloseRequest clutchUpdateRequest);
        Task<bool> UpdateClutch(ClutchUpdateRequest clutchUpdateRequest);
        void DeleteClutch(Clutch clutch);
        void DeleteClutchtById(object clutchId);
        Task<IEnumerable<ClutchResponse>> GetAllClutchsAsync();
        Task<IEnumerable<ClutchResponse>> GetClutchsByBreedingId(object breedingId);
        Task<IEnumerable<ClutchResponse>> GetClutchsByCreatedById(object CreatedById);
        Task<ClutchDetailResponse?> GetClutchByIdAsync(object clutchId);
    }
}

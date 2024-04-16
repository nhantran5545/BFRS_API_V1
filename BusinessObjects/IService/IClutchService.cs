using BusinessObjects.RequestModels.ClutchReqModels;
using BusinessObjects.ResponseModels.ClutchResModels;
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
        Task<int> CreateClutchAsync(ClutchAddRequest clutchAddRequest, int accountId);
        Task<bool> CloseClutch(int clutchId, ClutchCloseRequest clutchUpdateRequest, int accountId);
        Task<bool> UpdateClutch(int clutchId, ClutchUpdateRequest clutchUpdateRequest, int accountId);
        Task<IEnumerable<ClutchResponse>> GetAllClutchsAsync();
        Task<IEnumerable<ClutchResponse>> GetClutchsByBreedingId(object breedingId);
        Task<IEnumerable<ClutchResponse>> GetClutchsByCreatedById(object CreatedById);
        Task<ClutchDetailResponse?> GetClutchByIdAsync(object clutchId);
    }
}

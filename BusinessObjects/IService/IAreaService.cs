using BusinessObjects.RequestModels.AreaReqModels;
using BusinessObjects.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IAreaService
    {
        Task<int> CreateAreaAsync(AreaAddRequest areaAddRequest);
        Task<bool> UpdateAreaAsync(int areaId, AreaUpdateRequest areaUpdateRequest);
        Task<IEnumerable<AreaResponse>> GetAllAreaAsync();
        IEnumerable<AreaResponse> GetAreaByManagerId(int managerId);
        Task<AreaResponse?> GetAreaByIdAsync(object areaId);
    }
}

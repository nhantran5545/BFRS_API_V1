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
    public interface ICheckListDetailService
    {
        Task<CheckListDetailRequest> CreateCheckListAsync(CheckListDetailRequest checkListeDetail);
        Task<List<CheckListDetailResponse>> GetCheckListDetailsByCheckListId(int checkListId);
    }
}

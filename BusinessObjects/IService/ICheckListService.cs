using BusinessObjects.ResponseModels;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface ICheckListService
    {
        Task CreateCheckListAsync(CheckList checkList);
        void UpdateCheckList(CheckList checkList);
        Task<IEnumerable<CheckList>> GetAllCheckListsAsync();
        Task<CheckList?> GetCheckListByIdAsync(object checkListId);
    }
}

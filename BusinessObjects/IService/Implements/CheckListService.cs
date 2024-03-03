using DataAccess.IRepositories;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService.Implements
{
    public class CheckListService : ICheckListService
    {
        private readonly ICheckListRepository _checkListRepository;

        public CheckListService(ICheckListRepository checkListRepository)
        {
            _checkListRepository = checkListRepository;
        }

        public Task CreateCheckListAsync(CheckList checkList)
        {
            throw new NotImplementedException();
        }

        public void DeleteCheckList(CheckList checkList)
        {
            throw new NotImplementedException();
        }

        public void DeleteCheckListById(object checkListId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CheckList>> GetAllCheckListsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CheckList?> GetCheckListByIdAsync(object checkListId)
        {
            throw new NotImplementedException();
        }

        public void UpdateCheckList(CheckList checkList)
        {
            throw new NotImplementedException();
        }
    }
}

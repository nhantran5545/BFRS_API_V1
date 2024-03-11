﻿using DataAccess.IRepositories;
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

        public async Task CreateCheckListAsync(CheckList checkList)
        {
            await _checkListRepository.AddAsync(checkList);
            _checkListRepository.SaveChanges();
        }

        public void DeleteCheckList(CheckList checkList)
        {
            throw new NotImplementedException();
        }

        public void DeleteCheckListById(object checkListId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CheckList>> GetAllCheckListsAsync()
        {
            return await _checkListRepository.GetAllAsync();
        }

        public async Task<CheckList?> GetCheckListByIdAsync(object checkListId)
        {
            return await _checkListRepository.GetByIdAsync(checkListId);
        }

        public void UpdateCheckList(CheckList checkList)
        {
            throw new NotImplementedException();
        }
    }
}
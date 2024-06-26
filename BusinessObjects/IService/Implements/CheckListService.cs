﻿using AutoMapper;
using BusinessObjects.ResponseModels;
using DataAccess.IRepositories;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;

        public CheckListService(ICheckListRepository checkListRepository, IMapper mapper)
        {
            _checkListRepository = checkListRepository;
            _mapper = mapper;
        }

        public async Task CreateCheckListAsync(CheckList checkList)
        {
            await _checkListRepository.AddAsync(checkList);
            _checkListRepository.SaveChanges();
        }

        public async Task<IEnumerable<CheckList>> GetAllCheckListsAsync()
        {
            return await _checkListRepository.GetAllAsync();
        }

        public async Task<CheckList?> GetCheckListByIdAsync(object checkListId)
        {
            return await _checkListRepository.GetByIdAsync(checkListId);
        }

        public async Task<string> GetCheckListNameById(int checkListId)
        {
            var checkList = await _checkListRepository.GetByIdAsync(checkListId);
            return checkList?.CheckListName;
        }

        public void UpdateCheckList(CheckList checkList)
        {
            throw new NotImplementedException();
        }
    }
}

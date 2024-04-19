using BusinessObjects.RequestModels;
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
    public class CheckListDetailService : ICheckListDetailService
    {
        private readonly ICheckListDetailRepository _checkListDetailRepository;
        public CheckListDetailService(ICheckListDetailRepository checkListDetailRepository) 
        {
            _checkListDetailRepository = checkListDetailRepository;
        }
        public async Task<CheckListDetailRequest> CreateCheckListAsync(CheckListDetailRequest checkListDetailRequest)
        {
            if (checkListDetailRequest == null)
            {
                throw new ArgumentNullException(nameof(checkListDetailRequest), "CheckListDetailRequest cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(checkListDetailRequest.QuestionName))
            {
                throw new ArgumentException("QuestionName must not be null or empty.", nameof(checkListDetailRequest.QuestionName));
            }


            // Set default status
            checkListDetailRequest.Status = "ACTIVE";

            var checkListDetail = new CheckListDetail
            {
                CheckListId = checkListDetailRequest.CheckListId,
                QuestionName = checkListDetailRequest.QuestionName,
                Compulsory = checkListDetailRequest.Compulsory,
                Positive = checkListDetailRequest.Positive,
                Priority = checkListDetailRequest.Priority,
                Status = checkListDetailRequest.Status 
            };

            await _checkListDetailRepository.AddAsync(checkListDetail);
             _checkListDetailRepository.SaveChanges();

            return new CheckListDetailRequest
            {
                CheckListId = checkListDetail.CheckListId,
                QuestionName = checkListDetail.QuestionName,
                Compulsory = checkListDetail.Compulsory,
                Positive = checkListDetail.Positive,
                Priority = checkListDetail.Priority,
                Status = checkListDetail.Status 
            };
        }

        public async Task<List<CheckListDetail>> GetCheckListDetailsByCheckListId(int checkListId)
        {
            return await _checkListDetailRepository.GetCheckListDetailByCheckListId(checkListId);
        }
    }
}

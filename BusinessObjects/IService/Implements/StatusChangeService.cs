using DataAccess.IRepositories;
using DataAccess.IRepositories.Implements;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService.Implements
{
    public class StatusChangeService : IStatusChangeService
    {
        private readonly IBreedingStatusChangeRepository _breedingStatusChangeRepository;
        private readonly IClutchStatusChangeRepository _clutchStatusChangeRepository;
        private readonly IEggStatusChangeRepository _eggStatusChangeRepository;

        public StatusChangeService(IBreedingStatusChangeRepository breedingStatusChangeRepository, 
            IClutchStatusChangeRepository clutchStatusChangeRepository, 
            IEggStatusChangeRepository eggStatusChangeRepository)
        {
            _breedingStatusChangeRepository = breedingStatusChangeRepository;
            _clutchStatusChangeRepository = clutchStatusChangeRepository;
            _eggStatusChangeRepository = eggStatusChangeRepository;
        }

        public async Task AddBreedingChangeStatus(int breedingId, string? reason, int changedBy, string? oldStatus, string newStatus)
        {
            var breedingReason = new BreedingStatusChange()
            {
                BreedingId = breedingId,
                Description = reason,
                ChangedDate = DateTime.Now,
                ChangedBy = changedBy,
                OldStatus = oldStatus,
                NewStatus = newStatus
            };
            await _breedingStatusChangeRepository.AddAsync(breedingReason);
            _breedingStatusChangeRepository.SaveChanges();
        }

        public async Task AddClutchChangeStatus(int clutchId, string? reason, int changedBy, string? oldStatus, string newStatus)
        {
            var clutchReason = new ClutchStatusChange()
            {
                ClutchId = clutchId,
                Description = reason,
                ChangedDate = DateTime.Now,
                ChangedBy = changedBy,
                OldStatus = oldStatus,
                NewStatus = newStatus
            };
            await _clutchStatusChangeRepository.AddAsync(clutchReason);
            _clutchStatusChangeRepository.SaveChanges();
        }

        public async Task AddEggChangeStatus(int eggId, string? reason, int changedBy, string? oldStatus, string newStatus)
        {
            var eggReason = new EggStatusChange()
            {
                EggId = eggId,
                Description = reason,
                ChangedDate = DateTime.Now,
                ChangedBy = changedBy,
                OldStatus = oldStatus,
                NewStatus = newStatus
            };
            await _eggStatusChangeRepository.AddAsync(eggReason);
            _eggStatusChangeRepository.SaveChanges();
        }
    }
}

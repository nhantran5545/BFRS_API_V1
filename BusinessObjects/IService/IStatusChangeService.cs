using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService
{
    public interface IStatusChangeService
    {
        Task AddBreedingChangeStatus(int breedingId, string? reason, int changedBy, string? oldStatus, string newStatus);
        Task AddClutchChangeStatus(int clutchId, string? reason, int changedBy, string? oldStatus, string newStatus);
        Task AddEggChangeStatus(int eggId, string? reason, int changedBy, string? oldStatus, string newStatus);
    }
}

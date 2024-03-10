using DataAccess.IRepositories;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.IService.Implements
{
    internal class BirdAlgorithmService
    {
        private readonly IBirdRepository _birdRepository;
        private Dictionary<string, Guid> Ancestors = new Dictionary<string, Guid>();

        public BirdAlgorithmService(IBirdRepository birdRepository)
        {
            _birdRepository = birdRepository;
        }

        public async Task<Dictionary<string, Guid>> Pedigree(Guid birdId)
        {
            Ancestors.Add("", birdId);
            await TrackAncestorsAsync("", birdId);
            return Ancestors;
        }

        private async Task TrackAncestorsAsync(string ancestor, Guid birdId)
        {
            Bird? bird = await _birdRepository.GetByIdAsync(birdId);

            if (bird == null) { return; }

            if (bird.FatherBirdId != null)
            {
                string fatherAncestor = ancestor + "s";
                Guid FatherBirdId = bird.FatherBirdId.Value;
                Ancestors.Add(fatherAncestor, FatherBirdId);
                await TrackAncestorsAsync(fatherAncestor, FatherBirdId);
            }

            if (bird.MotherBirdId != null)
            {
                string motherAncestor = ancestor + "d";
                Guid MotherBirdId = bird.MotherBirdId.Value;
                Ancestors.Add(motherAncestor, MotherBirdId);
                await TrackAncestorsAsync(motherAncestor, MotherBirdId);
            }

            return;
        }
    }
}

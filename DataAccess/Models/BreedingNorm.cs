using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class BreedingNorm
    {
        public Guid BreedingNormId { get; set; }
        public Guid? BirdSpeciesId { get; set; }
        public DateTime? BreedingStartMonth { get; set; }
        public DateTime? BreedingEndMonth { get; set; }
        public string? WeatherFeatures { get; set; }
        public string? FoodRecommendation { get; set; }
        public double? PairingDurationMin { get; set; }
        public double? PairingDurationMax { get; set; }
        public double? NestingDurationMin { get; set; }
        public double? NestingDurationMax { get; set; }
        public double? IncubatingDurationMin { get; set; }
        public double? IncubatingDurationMax { get; set; }
        public double? NestTemperatureMin { get; set; }
        public double? NestTemperatureMax { get; set; }
        public double? NestHumidityMin { get; set; }
        public double? NestHumidityMax { get; set; }
        public string? Status { get; set; }

        public virtual BirdSpecy? BirdSpecies { get; set; }
    }
}

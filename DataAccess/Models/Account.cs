using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Account
    {
        public Account()
        {
            BreedingCreatedByNavigations = new HashSet<Breeding>();
            BreedingReasons = new HashSet<BreedingReason>();
            BreedingUpdatedByNavigations = new HashSet<Breeding>();
            Cages = new HashSet<Cage>();
            ClutchCreatedByNavigations = new HashSet<Clutch>();
            ClutchReasons = new HashSet<ClutchReason>();
            ClutchUpdatedByNavigations = new HashSet<Clutch>();
            EggCreatedByNavigations = new HashSet<Egg>();
            EggReasons = new HashSet<EggReason>();
            EggUpdatedByNavigations = new HashSet<Egg>();
            IssueAssignedToNavigations = new HashSet<Issue>();
            IssueCreatedByNavigations = new HashSet<Issue>();
            IssueUpdatedByNavigations = new HashSet<Issue>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AccountId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Role { get; set; }
        public Guid? FarmId { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Breeding> BreedingCreatedByNavigations { get; set; }
        public virtual ICollection<BreedingReason> BreedingReasons { get; set; }
        public virtual ICollection<Breeding> BreedingUpdatedByNavigations { get; set; }
        public virtual ICollection<Cage> Cages { get; set; }
        public virtual ICollection<Clutch> ClutchCreatedByNavigations { get; set; }
        public virtual ICollection<ClutchReason> ClutchReasons { get; set; }
        public virtual ICollection<Clutch> ClutchUpdatedByNavigations { get; set; }
        public virtual ICollection<Egg> EggCreatedByNavigations { get; set; }
        public virtual ICollection<EggReason> EggReasons { get; set; }
        public virtual ICollection<Egg> EggUpdatedByNavigations { get; set; }
        public virtual ICollection<Issue> IssueAssignedToNavigations { get; set; }
        public virtual ICollection<Issue> IssueCreatedByNavigations { get; set; }
        public virtual ICollection<Issue> IssueUpdatedByNavigations { get; set; }
    }
}

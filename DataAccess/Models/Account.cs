using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Account
    {
        public Account()
        {
            BreedingCreatedByNavigations = new HashSet<Breeding>();
            BreedingStatusChanges = new HashSet<BreedingStatusChange>();
            BreedingUpdatedByNavigations = new HashSet<Breeding>();
            Cages = new HashSet<Cage>();
            ClutchCreatedByNavigations = new HashSet<Clutch>();
            ClutchStatusChanges = new HashSet<ClutchStatusChange>();
            ClutchUpdatedByNavigations = new HashSet<Clutch>();
            EggCreatedByNavigations = new HashSet<Egg>();
            EggStatusChanges = new HashSet<EggStatusChange>();
            EggUpdatedByNavigations = new HashSet<Egg>();
            IssueAssignedToNavigations = new HashSet<Issue>();
            IssueCreatedByNavigations = new HashSet<Issue>();
            IssueUpdatedByNavigations = new HashSet<Issue>();
        }

        public int AccountId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Role { get; set; }
        public int? FarmId { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Breeding> BreedingCreatedByNavigations { get; set; }
        public virtual ICollection<BreedingStatusChange> BreedingStatusChanges { get; set; }
        public virtual ICollection<Breeding> BreedingUpdatedByNavigations { get; set; }
        public virtual ICollection<Cage> Cages { get; set; }
        public virtual ICollection<Clutch> ClutchCreatedByNavigations { get; set; }
        public virtual ICollection<ClutchStatusChange> ClutchStatusChanges { get; set; }
        public virtual ICollection<Clutch> ClutchUpdatedByNavigations { get; set; }
        public virtual ICollection<Egg> EggCreatedByNavigations { get; set; }
        public virtual ICollection<EggStatusChange> EggStatusChanges { get; set; }
        public virtual ICollection<Egg> EggUpdatedByNavigations { get; set; }
        public virtual ICollection<Issue> IssueAssignedToNavigations { get; set; }
        public virtual ICollection<Issue> IssueCreatedByNavigations { get; set; }
        public virtual ICollection<Issue> IssueUpdatedByNavigations { get; set; }
    }
}

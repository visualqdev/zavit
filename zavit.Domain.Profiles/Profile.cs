using System.Collections.Generic;
using zavit.Domain.Accounts;
using zavit.Domain.Profiles.Updating;
using zavit.Domain.Shared;

namespace zavit.Domain.Profiles
{
    public class Profile : IEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual Account Account { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual string About { get; set; }

        public virtual bool AcceptUpdate(ProfileUpdate profileUpdate, IEnumerable<IProfileUpdater> profileUpdaters)
        {
            var updated = false;
            foreach (var profileUpdater in profileUpdaters)
            {
                updated = profileUpdater.Update(this, profileUpdate) || updated;
            }

            return updated;
        }
    }
}
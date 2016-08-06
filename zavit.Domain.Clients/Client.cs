using System;
using zavit.Domain.Shared;

namespace zavit.Domain.Clients
{
    public class Client : IEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual string Secret { get; set; }
        public virtual string Name { get; set; }
        public virtual bool CanProvideSecret { get; set; }
        public virtual bool Active { get; set; }
        public virtual int RefreshTokenLifeTime { get; set; }
        public virtual string AllowedOrigin { get; set; }

        public virtual bool ValidateSecret(string clientSecret)
        {
            throw new System.NotImplementedException();
        }

        public virtual DateTime CalculateTokenExpiry(DateTime issueDateUtc)
        {
            return issueDateUtc.AddMinutes(RefreshTokenLifeTime);
        }
    }
}
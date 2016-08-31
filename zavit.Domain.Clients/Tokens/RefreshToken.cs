using System;
using zavit.Domain.Shared;

namespace zavit.Domain.Clients.Tokens
{
    public class RefreshToken : IEntity<Guid>
    {
        public virtual Guid Id { get; set; }
        public virtual string HashedTokenId { get; set; }
        public virtual string Subject { get; set; }
        public virtual Client Client { get; set; }
        public virtual string ProtectedTicket { get; set; }
        public virtual DateTime IssuedDateUtc { get; set; }
        public virtual DateTime ExpectedExpiryDateUtc { get; set; }
    }
}
using System.Collections.Generic;
using System.Linq;
using zavit.Domain.VenueMemberships;
using zavit.Web.Api.DtoFactories.VenueMemberships;
using zavit.Web.Api.Dtos.VenueMemberships;
using zavit.Web.Api.DtoServices.VenueMemberships.NewVenueMemberships;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.DtoServices.VenueMemberships
{
    public class VenueMembershipDtoService : IVenueMembershipDtoService
    {
        readonly IUserContext _userContext;
        readonly INewVenueMembershipProvider _newVenueMembershipProvider;
        readonly IVenueMembershipService _venueMembershipService;
        readonly IVenueMembershipDtoFactory _venueMembershipDtoFactory;

        public VenueMembershipDtoService(IUserContext userContext, INewVenueMembershipProvider newVenueMembershipProvider, IVenueMembershipService venueMembershipService, IVenueMembershipDtoFactory venueMembershipDtoFactory)
        {
            _userContext = userContext;
            _newVenueMembershipProvider = newVenueMembershipProvider;
            _venueMembershipService = venueMembershipService;
            _venueMembershipDtoFactory = venueMembershipDtoFactory;
        }

        public VenueMembershipDto AddVenueMembership(VenueMembershipDto venueMembershipDto)
        {
            var account = _userContext.Account;
            var newVenueMembership = _newVenueMembershipProvider.Provide(venueMembershipDto);

            var venueMembership = _venueMembershipService.AddUserToVenue(account, newVenueMembership);

            return _venueMembershipDtoFactory.CreateItem(venueMembership);
        }

        public IEnumerable<VenueMembershipDto> GetVenueMemberships()
        {
            var account = _userContext.Account;
            var venueMemberships = _venueMembershipService.GetVenueMembershipsForUser(account);

            var venueMembershipDtos = venueMemberships.Select(m => _venueMembershipDtoFactory.CreateItem(m));
            return venueMembershipDtos.ToList();
        }
    }
}
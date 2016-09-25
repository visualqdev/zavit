using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.VenueMemberships;
using zavit.Domain.VenueMemberships.NewVenueMembershipCreation;
using zavit.Web.Api.DtoFactories.VenueMemberships;
using zavit.Web.Api.Dtos.VenueMemberships;
using zavit.Web.Api.DtoServices.VenueMemberships;
using zavit.Web.Api.DtoServices.VenueMemberships.NewVenueMemberships;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.Tests.DtoServices.VenueMemberships 
{
    [Subject("VenueMembershipDtoService")]
    public class VenueMembershipDtoServiceTests : TestOf<VenueMembershipDtoService>
    {
        class When_adding_venue_membership
        {
            Because of = () => _result = Subject.AddVenueMembership(_venueMembershipDto);

            It should_return_venue_membership_dto_created_from_the_added_venue_membership = 
                () => _result.ShouldEqual(_newVenueMembershipDto);

            Establish context = () =>
            {
                _venueMembershipDto = NewInstanceOf<VenueMembershipDto>();
                
                var account = NewInstanceOf<Account>();
                Injected<IUserContext>().Stub(c => c.Account).Return(account);

                var newVenueMembership = NewInstanceOf<NewVenueMembership>();
                Injected<INewVenueMembershipProvider>().Stub(p => p.Provide(_venueMembershipDto)).Return(newVenueMembership);

                var venueMembership = NewInstanceOf<VenueMembership>();
                Injected<IVenueMembershipService>().Stub(s => s.AddUserToVenue(account, newVenueMembership)).Return(venueMembership);

                _newVenueMembershipDto = NewInstanceOf<VenueMembershipDto>();
                Injected<IVenueMembershipDtoFactory>().Stub(f => f.CreateItem(venueMembership)).Return(_newVenueMembershipDto);
            };

            static VenueMembershipDto _result;
            static VenueMembershipDto _venueMembershipDto;
            static VenueMembershipDto _newVenueMembershipDto;
        }

        class When_getting_venue_memberships
        {
            Because of = () => _result = Subject.GetVenueMemberships();

            It should_return_a_collection_of_venue_memberships = () => _result.ShouldContainOnly(_venueMembershipDto, _otherVenueMembershipDto);

            Establish context = () =>
            {
                var account = NewInstanceOf<Account>();
                Injected<IUserContext>().Stub(c => c.Account).Return(account);

                var venueMembership = NewInstanceOf<VenueMembership>();
                var otherVenueMembership = NewInstanceOf<VenueMembership>();

                Injected<IVenueMembershipService>()
                    .Stub(s => s.GetVenueMemberships(account))
                    .Return(new [] { venueMembership, otherVenueMembership });

                _venueMembershipDto = NewInstanceOf<VenueMembershipDto>();
                _otherVenueMembershipDto = NewInstanceOf<VenueMembershipDto>();

                Injected<IVenueMembershipDtoFactory>()
                    .Stub(f => f.CreateItem(venueMembership))
                    .Return(_venueMembershipDto);

                Injected<IVenueMembershipDtoFactory>()
                    .Stub(f => f.CreateItem(otherVenueMembership))
                    .Return(_otherVenueMembershipDto);
            };

            static IEnumerable<VenueMembershipDto> _result;
            static VenueMembershipDto _venueMembershipDto;
            static VenueMembershipDto _otherVenueMembershipDto;
        }
    }
}


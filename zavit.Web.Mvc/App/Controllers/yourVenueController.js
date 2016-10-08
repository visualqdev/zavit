import * as IndexView from "../views/yourVenue/indexView";
import * as VenueMembersPartial from "../views/yourVenue/venueMembersPartial";
import * as MainContent from "../layout/mainContent";
import * as Routes from "../routing/routes";
import * as PostLoginRedirect from "../modules/account/postLoginRedirect";
import * as YourVenueMap from "../modules/venues/yourVenueMap";
import * as VenueMembershipClient from "../modules/venues/venueMembershipClient";

export function index(venueId) {
    MainContent.load();

    VenueMembershipClient
        .getVenueMembership(venueId)
        .then(membership => {
            const view = IndexView.getView(membership);
            MainContent.append(view);
            YourVenueMap.addMapTo(".yourVenueMap");
            return VenueMembershipClient.getVenueMembers(venueId, 0, 20);
        })
        .then(venueMembersResult => {
            const venueMembersPartial = VenueMembersPartial.getView(venueMembersResult.Members);
            $("#yourVenueMembers").append(venueMembersPartial);
            enableLoadMore(venueMembersResult, 20);
        })
        .catch((error) => {
            if (error && error.status && error.status === 401) {
                PostLoginRedirect.storeRedirectUrl(window.location.href);
                Routes.goTo(Routes.login);
            }
        });

    //const view = IndexView.getView({
    //    Venue: {
    //        Name: "Test venue",
    //        Address: "1 venue address, In This Town, PO54 3OD, United World",
    //        Longitude: -0.281345,
    //        Latitude: 51.493213,
    //        Activities: [
    //            {
    //                Id: 1,
    //                Name: "Aerobics"
    //            },
    //            {
    //                Id: 2,
    //                Name: "Aikido"
    //            },
    //            {
    //                Id: 2,
    //                Name: "American Football"
    //            },
    //            {
    //                Id: 2,
    //                Name: "Angling"
    //            },
    //            {
    //                Id: 2,
    //                Name: "Aquathon"
    //            },
    //            {
    //                Id: 2,
    //                Name: "Archery"
    //            },
    //            {
    //                Id: 2,
    //                Name: "Arm Wrestling"
    //            },
    //            {
    //                Id: 2,
    //                Name: "Athletics"
    //            },
    //            {
    //                Id: 2,
    //                Name: "Australian Rules Football"
    //            }
    //        ]
    //    },
    //    Activities: [
    //        {
    //            Id: 1,
    //            Name: "Aerobics"
    //        }
    //    ]
    //});
    //MainContent.append(view);
    //YourVenueMap.addMapTo(".yourVenueMap");

    const venueMembers = [
    {
        Id: 1,
        DisplayName: "John Paul",
        Activities: [
            {
                Id: 2,
                Name: "American Football"
            },
            {
                Id: 2,
                Name: "Angling"
            },
            {
                Id: 2,
                Name: "Aquathon"
            }
        ]
    },
    {
        Id: 2,
        DisplayName: "Freddy Falcon",
        Activities: [
            {
                Id: 2,
                Name: "Aquathon"
            },
            {
                Id: 2,
                Name: "Archery"
            }
        ]
    },
    {
        Id: 3,
        DisplayName: "Roger Smith",
        Activities: [
            {
                Id: 2,
                Name: "American Football"
            },
            {
                Id: 2,
                Name: "Angling"
            },
            {
                Id: 2,
                Name: "Aquathon"
            }
        ]
    }];

    const venueMembersPartial = VenueMembersPartial.getView(venueMembers);
    $("#yourVenueMembers").append(venueMembersPartial);
}

function enableLoadMore(venueMembersResult, skip) {
    alert("Load more users will be implemented");
}
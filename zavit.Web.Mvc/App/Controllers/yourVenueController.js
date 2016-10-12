import * as IndexView from "../views/yourVenue/indexView";
import * as VenueMembersPartial from "../views/yourVenue/venueMembersPartial";
import * as MainContent from "../layout/mainContent";
import * as Routes from "../routing/routes";
import * as PostLoginRedirect from "../modules/account/postLoginRedirect";
import * as YourVenueMap from "../modules/venues/yourVenueMap";
import * as VenueMembershipClient from "../modules/venues/venueMembershipClient";
import * as Progress from "../modules/loading/progress";

export function index(venueId) {
    MainContent.load(Routes.yourVenue);

    Progress.start();

    VenueMembershipClient
        .getVenueMembership(venueId)
        .then(membership => {
            const view = IndexView.getView(membership);
            MainContent.append(view);
            YourVenueMap.addMapTo(".yourVenueMap");
            return VenueMembershipClient.getVenueMembers(venueId, 0, 6);
        })
        .then(venueMembersResult => {
            processVenueMembersResult(venueMembersResult, venueId);
        })
        .catch((error) => {
            checkUnauthorised(error);
        })
        .then(Progress.done);
}

function enableLoadMore(venueMembersResult, venueId) {
    if (!venueMembersResult.HasMoreResults) return;

    $("#mainContent").on("scroll", () => {
        if($("#mainContent").scrollTop() + $("#mainContent").height() > $(document).height()) {
            $("#mainContent").off("scroll");

            Progress.start();

            VenueMembershipClient
                .getVenueMembers(venueId, venueMembersResult.Take, venueMembersResult.Take)
                .then(venueMembersMoreResult => {
                    processVenueMembersResult(venueMembersMoreResult, venueId);
                })
                .catch((error) => {
                    checkUnauthorised(error);
                })
                .then(Progress.done);
        }
    });
}

function processVenueMembersResult(venueMembersResult, venueId) {
    const venueMembersPartial = VenueMembersPartial.getView(venueMembersResult.Members);
    $("#yourVenueMembers").append(venueMembersPartial);
    enableLoadMore(venueMembersResult, venueId);
}

function checkUnauthorised(errot) {
    if (error && error.status && error.status === 401) {
        PostLoginRedirect.storeRedirectUrl(window.location.href);
        Routes.goTo(Routes.login);
    }
}
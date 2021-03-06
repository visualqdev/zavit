﻿import * as IndexView from "../views/yourVenue/indexView";
import * as VenueActivitiesPartial from "../views/yourVenue/venueActivitiesPartial";
import * as VenueMembersPartial from "../views/yourVenue/venueMembersPartial";
import * as MainContent from "../layout/mainContent";
import * as Routes from "../routing/routes";
import * as PostLoginRedirect from "../modules/account/postLoginRedirect";
import * as YourVenueMap from "../modules/venues/yourVenueMap";
import * as VenueMembershipService from "../modules/venues/venueMembershipService";
import * as Progress from "../modules/loading/progress";
import * as ActivityClient from "../modules/activities/activityClient";
import * as VenueService from "../modules/venues/venueService";
import * as Info from "../modules/loading/info";

const venueMembersTake = 20;

export function index(options) {
    MainContent.load(Routes.yourVenue);

    Progress.start();

    VenueMembershipService
        .getVenueMembership(options)
        .then(membership => {
            const view = IndexView.getView(membership.membershipDetails, membership.allOtherActivities);
            MainContent.append(view);
            YourVenueMap.addMapTo(".yourVenueMap");
            AttachActivityEvents(membership);
            return VenueMembershipService.getVenueMembers(options, 0, venueMembersTake);
        })
        .then(venueMembersResult => {
            processVenueMembersResult(venueMembersResult, options);
        })
        .catch((error) => {
            checkUnauthorised(error);
        })
        .then(Progress.done);
}

function enableLoadMore(venueMembersResult, options) {
    if (!venueMembersResult.HasMoreResults) return;

    $("#mainContent").on("scroll", () => {
        if($("#mainContent").scrollTop() + $("#mainContent").height() > $(document).height()) {
            $("#mainContent").off("scroll");

            Progress.start();

            VenueMembershipService
                .getVenueMembers(options, venueMembersResult.Take, venueMembersTake)
                .then(venueMembersMoreResult => {
                    processVenueMembersResult(venueMembersMoreResult, options);
                })
                .catch((error) => {
                    checkUnauthorised(error);
                })
                .then(Progress.done);
        }
    });
}

function processVenueMembersResult(venueMembersResult, options) {
    const venueMembersPartial = VenueMembersPartial.getView(venueMembersResult.Members);
    $("#yourVenueMembers").append(venueMembersPartial);
    enableLoadMore(venueMembersResult, options);
}

function checkUnauthorised(error) {
    if (error && error.status && error.status === 401) {
        PostLoginRedirect.storeRedirectUrl(window.location.href);
        Routes.goTo(Routes.login);
    }
}

function AttachActivityEvents(membership) {

    $("#yourVenueOtherActivities").click(e => {
        e.preventDefault();
        ActivityClient
        .getAllActivities()
        .then(activities => {
            const allActivitiesMarkup = VenueActivitiesPartial.getView(activities, membership.Activities, true);
            $(".yourVenueActivities").replaceWith(allActivitiesMarkup);
        });
    });

    $('.selectedActivities').delegate('a', 'click', e => {
        e.preventDefault();;
        $('input:checkbox[value="' + $(e.currentTarget).attr('data-id') + '"]').trigger('click');
    });

    function informUser(event) {
        if ($(event.currentTarget).is(':checked')) {
            const activitySelected = $(event.currentTarget).parent().text();
            Info.provide(`<p><strong>Other members can now find you for ${activitySelected}</strong></p>`);
        } else {
            const activityDeselected = $(event.currentTarget).parent().text();
            Info.provide(`<p><strong>Other members can no longer find you for ${activityDeselected}</strong></p>`);
        }
    }

    $("#yourVenue [name='venueActivities']").on("change", (e) => { informUser(e) });

    $("#yourVenue")
        .on("change",
            "[name='venueActivities']",
            (e) => {
                
        const activitiCheckboxes = $("#yourVenue [name='venueActivities']:checked");
        const activities = activitiCheckboxes
            .map((index, checkbox) => $(checkbox).val())
            .get();

        VenueService.joinVenue({
                activities,
                venueId: membership.membershipDetails.Venue.Id,
                placeId: membership.membershipDetails.Venue.PublicPlaceId
        }).then(updatedMembership => {
            VenueActivitiesPartial.updateActivities(updatedMembership.Activities);
        });
    });

}
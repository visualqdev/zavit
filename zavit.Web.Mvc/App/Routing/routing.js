﻿import * as Routes from "./routes";
import * as TopNav from "../modules/navigation/topNav";
import * as HomeController from "../controllers/homeController";
import * as LoginController from "../controllers/loginController";
import * as ExternalLoginController from "../controllers/externalLoginController";
import * as VenueController from "../controllers/venueController";
import * as PostLoginRedirect from "../modules/account/postLoginRedirect";
import * as YourVenuesController from "../controllers/yourVenuesController";
import * as YourVenueController from "../controllers/yourVenueController";
import * as MessageInboxController from "../controllers/messageInboxController";
import * as ProfileController from "../controllers/profileController";

export function registerRoutes() {
    crossroads.addRoute(`/${Routes.home}`, () => {
        if (PostLoginRedirect.processRedirect()) return;

        TopNav.navigatedToRoute(Routes.home);
        HomeController.explore();
    });

    crossroads.addRoute(`/${Routes.home}/test`, () => {
        if (PostLoginRedirect.processRedirect()) return;

        TopNav.navigatedToRoute(Routes.home);
        HomeController.index();
    });

    crossroads.addRoute(`/${Routes.login}`, () => LoginController.login());

    crossroads.addRoute(`/${Routes.logout}`, () => {
        LoginController.logout();
        Routes.goTo(Routes.home);
    });

    crossroads.addRoute("/externallogin{?query}", (query) => {
        ExternalLoginController.processExternalLogin({
            externalAccessToken: query.externalaccesstoken,
            provider: query.provider,
            externalUsername: query.externalusername,
            externalEmail: query.externalemail,
            hasLocalAccount: query.haslocalaccount
        });
    });

    crossroads.addRoute(`/${Routes.joinVenue}`, () => VenueController.joinVenue());
   
    crossroads.addRoute(`/${Routes.yourVenues}`, () => {
        TopNav.navigatedToRoute(Routes.yourVenues);
        YourVenuesController.index();
    });

    crossroads.addRoute(`/${Routes.yourVenue}/:venueId::?query:`, (venueId, query) => {
        TopNav.navigatedToRoute(Routes.yourVenues);
        YourVenueController.index({
            venueId,
            publicPlaceId: query && query.placeid
        });
    });

    crossroads.addRoute(`/${Routes.messageInbox}:?query:`, (query) => {
        TopNav.navigatedToRoute(Routes.messageInbox);
        MessageInboxController.index({
            accountIds: query && query.accounts ? query.accounts.split(',') : [],
            threadId: query && query.threadid
        });
    });

    crossroads.addRoute(`/${Routes.profile}`, () => ProfileController.index());

    function parseHash(newHash, oldHash) {
        crossroads.parse(newHash);
    }

    hasher.initialized.add(parseHash);
    hasher.changed.add(parseHash); 
    hasher.init();
}
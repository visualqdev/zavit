import * as Routes from "./routes";
import * as Event from "../events/event";
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
        Event.pageLoaded(`/${Routes.home}`, "Home");
        HomeController.explore();
    });

    crossroads.addRoute(`/${Routes.home}/test`, () => {
        if (PostLoginRedirect.processRedirect()) return;

        TopNav.navigatedToRoute(Routes.home);
        Event.pageLoaded(`/${Routes.home}`, "Home");
        HomeController.index();
    });

    crossroads.addRoute(`/${Routes.login}`, () => {
        Event.pageLoaded(`/${Routes.login}`, "Login");
        LoginController.login();
    });

    crossroads.addRoute(`/${Routes.logout}`, () => {
        LoginController.logout();
        track(`/${Routes.logout}`, "Logout");
        Routes.goTo(Routes.home);
    });

    crossroads.addRoute("/externallogin{?query}", (query) => {
        Event.pageLoaded(`/externallogin`, "External login");
        ExternalLoginController.processExternalLogin({
            externalAccessToken: query.externalaccesstoken,
            provider: query.provider,
            externalUsername: query.externalusername,
            externalEmail: query.externalemail,
            hasLocalAccount: query.haslocalaccount
        });
    });

    crossroads.addRoute(`/${Routes.joinVenue}`, () => {
        Event.pageLoaded(`/${Routes.joinVenue}`, "Join venue");
         VenueController.joinVenue();
    });
   
    crossroads.addRoute(`/${Routes.yourVenues}`, () => {
        TopNav.navigatedToRoute(Routes.yourVenues);
        Event.pageLoaded(`/${Routes.yourVenues}`, "Your venues");
        YourVenuesController.index();
    });

    crossroads.addRoute(`/${Routes.yourVenue}/:venueId::?query:`, (venueId, query) => {
        TopNav.navigatedToRoute(Routes.yourVenues);
        Event.pageLoaded(`/${Routes.yourVenue}`, "Your venue");
        YourVenueController.index({
            venueId,
            publicPlaceId: query && query.placeid
        });
    });

    crossroads.addRoute(`/${Routes.messageInbox}:?query:`, (query) => {
        TopNav.navigatedToRoute(Routes.messageInbox);
        Event.pageLoaded(`/${Routes.messageInbox}`, "Message Inbox");
        MessageInboxController.index({
            accountIds: query && query.accounts ? query.accounts.split(',') : [],
            threadId: query && query.threadid
        });
    });

    crossroads.addRoute(`/${Routes.profile}`, () => {
        Event.pageLoaded(`/${Routes.profile}`, "Profile");
        ProfileController.index();
    });

    function parseHash(newHash, oldHash) {
        crossroads.parse(newHash);
    }

    hasher.initialized.add(parseHash);
    hasher.changed.add(parseHash); 
    hasher.init();
}
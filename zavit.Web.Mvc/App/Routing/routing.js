import * as Routes from "./routes";
import * as HomeController from "../controllers/homeController";
import * as LoginController from "../controllers/loginController";
import * as ExternalLoginController from "../controllers/externalLoginController";
import * as VenueController from "../controllers/venueController";
import * as PostLoginRedirect from "../modules/account/postLoginRedirect";

export function registerRoutes() {
    crossroads.addRoute(`/${Routes.home}`, () => {
        if (PostLoginRedirect.processRedirect()) return;
        HomeController.explore();
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
   
    function parseHash(newHash, oldHash) {
        crossroads.parse(newHash);
    }

    hasher.initialized.add(parseHash);
    hasher.changed.add(parseHash); 
    hasher.init();
}
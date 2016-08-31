import * as HomeController from "../controllers/homeController";
import * as LoginController from "../controllers/loginController"
import * as ExternalLoginController from "../controllers/externalLoginController"

export function registerRoutes() {

    crossroads.addRoute("/", () => HomeController.explore());

    crossroads.addRoute("/login", function () {
        LoginController.login();
    });

    crossroads.addRoute("/logout", function () {
        LoginController.logout();
        window.location.href = "/";
    });

    crossroads.addRoute("/externallogin{?query}", function(query) {
        ExternalLoginController.processExternalLogin({
            externalAccessToken: query.externalaccesstoken,
            provider: query.provider,
            externalUsername: query.externalusername,
            externalEmail: query.externalemail,
            hasLocalAccount: query.haslocalaccount
        });
    });
   
    function parseHash(newHash, oldHash) {
        crossroads.parse(newHash);
    }

    hasher.initialized.add(parseHash);
    hasher.changed.add(parseHash); 
    hasher.init();
}
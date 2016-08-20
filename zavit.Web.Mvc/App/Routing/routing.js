import * as HomeController from "../controllers/homeController";
import * as LoginController from "../controllers/loginController"

export function registerRoutes() {

    crossroads.addRoute("/", () => {
        HomeController.explore();
    });

    crossroads.addRoute("/login", function () {
        LoginController.login();
    });

    crossroads.addRoute("/logout", function () {
        LoginController.logout();
    });
   
    function parseHash(newHash, oldHash) {
        crossroads.parse(newHash);
    }

    hasher.initialized.add(parseHash);
    hasher.changed.add(parseHash); 
    hasher.init();
}
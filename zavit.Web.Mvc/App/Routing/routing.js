import * as HomeController from "../controllers/homeController";

export function registerRoutes() {

    crossroads.addRoute("/", () => HomeController.explore());

    crossroads.addRoute("/login", function () {
        $.loginController().login();
    });
   
    function parseHash(newHash, oldHash) {
        crossroads.parse(newHash);
    }

    hasher.initialized.add(parseHash);
    hasher.changed.add(parseHash); 
    hasher.init();
}
import * as HomeController from "../controllers/homeController";

export function registerRoutes() {

    crossroads.addRoute("/", () => {
         HomeController.explore();
    });
   
    function parseHash(newHash, oldHash) {
        crossroads.parse(newHash);
    }

    hasher.initialized.add(parseHash);
    hasher.changed.add(parseHash); 
    hasher.init();
}
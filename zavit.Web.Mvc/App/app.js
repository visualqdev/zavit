import * as Routing from "./routing/routing";
import * as TopNav from "./modules/navigation/topNav";

(function() {
    TopNav.initialize();
    Routing.registerRoutes();
}())
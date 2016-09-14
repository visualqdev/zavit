import * as Routing from "./routing/routing";
import * as Search from "./navigation/search"
import * as TopNav from "./modules/navigation/topNav";

(function() {
    TopNav.initialize();
    Routing.registerRoutes();
    Search.initialise();
}())
import * as Routing from './routing/routing';
import * as Search from './navigation/search'

(function() {
    
    Routing.registerRoutes();
    Search.search();
}())
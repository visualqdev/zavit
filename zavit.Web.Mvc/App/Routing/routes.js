export const login = "login";
export const logout = "logout";
export const home = "";
export const joinVenue = "joinvenue";
export const yourVenues = "yourvenues";

export function goTo(routeName) {
    hasher.setHash(routeName);
}
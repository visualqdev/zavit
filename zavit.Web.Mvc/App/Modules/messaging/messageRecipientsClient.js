import * as ApiSettings from "../settings/apiSettings";
import * as AuthorizedClient from "../clients/authorizedClient";

export function getRecipientsById(accountIds) {
    const getRecipientsUrl = `${ApiSettings.apiUrl}api/messagerecipients?accountids=${accountIds.join("&accountids=")}`;
    
    return AuthorizedClient
        .send({
            url: getRecipientsUrl,
            type: "get",
            contentType: "application/json; charset=utf-8"
        });
}

export function suggestRecipients(searchTerm, skip, take) {
    const getRecipientsUrl = `${ApiSettings.apiUrl}api/messagerecipientsuggestions?searchterm=${searchTerm}&skip=${skip}&take=${take}`;
    
    return AuthorizedClient
        .send({
            url: getRecipientsUrl,
            type: "get",
            contentType: "application/json; charset=utf-8"
        });
}
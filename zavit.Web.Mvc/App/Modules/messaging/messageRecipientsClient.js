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
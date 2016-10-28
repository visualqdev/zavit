import * as ApiSettings from "../settings/apiSettings";
import * as AuthorizedClient from "../clients/authorizedClient";

export function sendMessage(threadId, messageBody) {
    const postMessageUrl = `${ApiSettings.apiUrl}api/messagethreads/${threadId}/messages`;
    const data = {
        body: messageBody
    };
    
    return AuthorizedClient
        .send({
            url: postMessageUrl,
            type: "post",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data)
        });
}
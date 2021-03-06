﻿import * as ApiSettings from "../settings/apiSettings";
import * as AuthorizedClient from "../clients/authorizedClient";

export function sendMessage(threadId, message) {
    const postMessageUrl = `${ApiSettings.apiUrl}api/messagethreads/${threadId}/messages`;
    
    return AuthorizedClient
        .send({
            url: postMessageUrl,
            type: "post",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(message)
        });
}

export function confirmMessageRead(messageStamp) {
    const confirmMessageReadUrl = `${ApiSettings.apiUrl}api/messages/${messageStamp}/statuses`;

    const data = {
        status: "Read"
    };

    return AuthorizedClient
        .send({
            url: confirmMessageReadUrl,
            type: "post",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data)
        });
}
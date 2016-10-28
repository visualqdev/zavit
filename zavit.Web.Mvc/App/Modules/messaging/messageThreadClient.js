import * as ApiSettings from "../settings/apiSettings";
import * as AuthorizedClient from "../clients/authorizedClient";

export function startNewThread(options) {
    const postMessageThreadUrl = `${ApiSettings.apiUrl}api/messagethreads/new`;
    const data = {
        thread: {
            participants: options.recipients
        },
        message: {
            body: options.messageBody
        }
    };

    return AuthorizedClient
        .send({
            url: postMessageThreadUrl,
            type: "post",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data)
        });
}

export function getMessageThreads() {
    const getMessageThreadsUrl = `${ApiSettings.apiUrl}api/messagethreads`;
    
    return AuthorizedClient
        .send({
            url: getMessageThreadsUrl,
            type: "get",
            contentType: "application/json; charset=utf-8"
        });
}

export function getMessageThread(threadId) {
    const getMessageThreadUrl = `${ApiSettings.apiUrl}api/messagethreads/${threadId}`;
    
    return AuthorizedClient
        .send({
            url: getMessageThreadUrl,
            type: "get",
            contentType: "application/json; charset=utf-8"
        });
}
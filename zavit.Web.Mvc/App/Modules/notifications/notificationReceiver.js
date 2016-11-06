import * as AccountService from "../account/accountService";

const inboxObservers = {};
let threadObserver;
let connectionStarted = false;
let messagingHubProxy;

export function observeInbox(observerId, callback) {
    if (Object.keys(inboxObservers).length > 0)
        inboxObservers[observerId] = callback;
    else {
        startSignalRConnection()
            .then(() => {
                const userAccount = AccountService.currentUserAccount();
                messagingHubProxy.server.joinInboxNotifications(1);
            });
    }
}

export function observeThread(threadId, callback) {
    if (threadObserver)
        messagingHubProxy.server.leaveThreadNotifications(1, threadObserver.threadId);
    
    startSignalRConnection()
        .then(() => {
            threadObserver = {
                threadId,
                callback
            };
            messagingHubProxy.server.joinThreadNotifications(1, threadId);
        });
}

function notifyInboxNewMessage(message) {
    const messageJson = JSON.parse(message);

    for (let observerId in inboxObservers) {
        inboxObservers[observerId](messageJson);
    }
}

function notifyThreadNewMessage(message) {
    threadObserver.callback(JSON.parse(message));
}

function startSignalRConnection() {
    return new Promise((resolve, reject) => {
        if (connectionStarted) resolve();

        connectionStarted = true;
        messagingHubProxy = $.connection.messagingHub;

        messagingHubProxy.client.inboxNewMessage = notifyInboxNewMessage;
        messagingHubProxy.client.threadNewMessage = notifyThreadNewMessage;
                    
        $.connection.hub.start()
            .done(resolve)
            .fail(() => {
                connectionStarted = false;
                reject();
            });
    });
}
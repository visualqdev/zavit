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
                messagingHubProxy.server.joinInboxNotifications(userAccount.accountId);
            });
    }
}

export function observeThread(options) {
    const userAccount = AccountService.currentUserAccount();

    if (threadObserver)
        messagingHubProxy.server.leaveThreadNotifications(userAccount.accountId, threadObserver.threadId);
    
    startSignalRConnection()
        .then(() => {
            threadObserver = options;
            messagingHubProxy.server.joinThreadNotifications(userAccount.accountId, threadObserver.threadId);
        });
}

function notifyInboxNewMessage(message) {
    const messageJson = JSON.parse(message);

    for (let observerId in inboxObservers) {
        inboxObservers[observerId](messageJson);
    }
}

function notifyThreadNewMessage(message) {
    threadObserver.threadNewMessage(JSON.parse(message));
}

function notifyThreadMessagesRead(messagesRead) {
    threadObserver.threadMessagesRead(JSON.parse(messagesRead));
}

function startSignalRConnection() {
    return new Promise((resolve, reject) => {
        if (connectionStarted) resolve();

        connectionStarted = true;
        messagingHubProxy = $.connection.messagingHub;

        messagingHubProxy.client.inboxNewMessage = notifyInboxNewMessage;
        messagingHubProxy.client.threadNewMessage = notifyThreadNewMessage;
        messagingHubProxy.client.threadMessagesRead = notifyThreadMessagesRead;

        $.connection.hub.start()
            .done(resolve)
            .fail(() => {
                connectionStarted = false;
                reject();
            });
    });
}
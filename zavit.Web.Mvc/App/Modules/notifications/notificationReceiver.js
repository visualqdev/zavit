import * as AccountService from "../account/accountService";

const inboxObservers = {};
let threadObserver;
let connectionStarted = false;
let connectionStarting = false;
let connectionStartedObservers = [];
let messagingHubProxy;

export function observeInbox(observerId, callback) {
    if (Object.keys(inboxObservers).length > 0)
        inboxObservers[observerId] = callback;
    else {
        startSignalRConnection()
            .then(() => {
                const userAccount = AccountService.currentUserAccount();
                messagingHubProxy.server.joinInboxNotifications(userAccount.accountId);
                inboxObservers[observerId] = callback;
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
        if (connectionStarted) {
            resolve();
        }
        else if (connectionStarting) {
            connectionStartedObservers.push(resolve);
        } else {
            messagingHubProxy = $.connection.messagingHub;

            messagingHubProxy.client.inboxNewMessage = notifyInboxNewMessage;
            messagingHubProxy.client.threadNewMessage = notifyThreadNewMessage;
            messagingHubProxy.client.threadMessagesRead = notifyThreadMessagesRead;

            $.connection.hub.start()
                .done(() => {
                    connectionStarted = true;
                    connectionStarting = false;
                    connectionStartedObservers.forEach(observer => observer());
                    resolve();
                })
                .fail(() => {
                    connectionStarted = false;
                    connectionStarting = false;
                    reject();
                });
        }
    });
}
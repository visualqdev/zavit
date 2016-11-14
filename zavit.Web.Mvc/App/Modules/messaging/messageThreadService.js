import * as MessageRecipientsClient from "./messageRecipientsClient";
import * as MessageThreadClient from "./messageThreadClient";
import * as MessageClient from "./messageClient";

export function startNewThread(options) {
    return MessageThreadClient.startNewThread(options);
}

export function getInboxThread(options, messageThreads) {
    if (options.threadId) {
        return MessageThreadClient.getMessageThread(options.threadId);
    }
    else if (isNewThread(options)) {
        return new Promise((resolve, reject) => {
            MessageRecipientsClient
                .getRecipientsById(options.accountIds)
                .then(recipients => {
                    const emptyNewThread = createEmptyThread(recipients);
                    resolve(emptyNewThread);
                });
        });
    } else if (messageThreads && messageThreads.length > 0){
        return MessageThreadClient.getMessageThread(messageThreads[0].ThreadId);
    }
}

export function sendMessage(sendMessageRequest) {
    if (sendMessageRequest.inboxThread.ThreadId) {
        return new Promise((resolve, reject) => {
            MessageClient
                .sendMessage(sendMessageRequest.inboxThread.ThreadId, sendMessageRequest.message)
                .then(message => resolve({
                    message,
                    inboxThread: sendMessageRequest.inboxThread
                }));
        });
    }
    else if (sendMessageRequest.inboxThread.Recipients) {
        return new Promise((resolve, reject) => {
            MessageThreadClient
                .startNewThread({
                    recipients: sendMessageRequest.inboxThread.Recipients,
                    message: sendMessageRequest.message
                })
                .then(newMessageThreadResponse => resolve({
                    message: newMessageThreadResponse.Message,
                    inboxThread: newMessageThreadResponse.Thread
                })); 
        });
    }
}

export function confirmMessageRead(messageStamp) {
    MessageClient.confirmMessageRead(messageStamp);
}

function createEmptyThread(recipients) {
    const threadTitle = recipients
                         .map(recipient => recipient.DisplayName)
                         .join(", ");

    return {
        ThreadTitle: threadTitle,
        Recipients: recipients,
        MessagesCollection: {
            HasMoreResults: false,
            Take: 0,
            Messages: []
        }
    };
}

function isNewThread(options) {
    return options.accountIds && options.accountIds.length && options.accountIds.length > 0;
}
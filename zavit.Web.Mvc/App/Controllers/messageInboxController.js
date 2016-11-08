import * as MainContent from "../layout/mainContent";
import * as Routes from "../routing/routes";
import * as Progress from "../modules/loading/progress";
import * as IndexView from "../views/messageInbox/indexView";
import * as MessagePartial from "../views/messageInbox/messagePartial";
import * as MessageThreadPartial from "../views/messageInbox/messageThreadPartial";
import * as MessageThreadService from "../modules/messaging/messageThreadService";
import * as MessageInboxService from "../modules/messaging/messageInboxService";
import * as NewMessageFactory from "../modules/messaging/newMessageFactory";
import * as MessageLayout from "../modules/messaging/messageLayout";
import * as NotificationReceiver from "../modules/notifications/notificationReceiver";
import * as PostLoginRedirect from "../modules/account/postLoginRedirect";

let currentInboxThread;

export function index(options) {
    MainContent.load(Routes.messageInbox);
    Progress.start();

    MessageInboxService
        .getInboxThreads()
        .then(messageThreads => {
            const view = IndexView.getView(messageThreads);
            MainContent.append(view);

            attachInboxEvents();
            return MessageThreadService.getInboxThread(options);
        })
        .then(inboxThread => {
            showInboxThread(inboxThread);
        })
        .then(MessageLayout.setUp)
        .catch((error) => {
            checkUnauthorised(error);
        })
        .then(Progress.done);
}



function attachInboxEvents() {
    $("#messageThreads").on("click", "[data-thread-id]", (e) => {
        e.preventDefault();
        MessageLayout.styleSelected(e, $(this));
        const threadId = $(e.target).attr("data-thread-id");
        MessageThreadService.getInboxThread({
                threadId
            })
            .then(inboxThread => {
                showInboxThread(inboxThread);
            });
    });
}

function showInboxThread(inboxThread) {
    const threadView = MessageThreadPartial.getView(inboxThread);
    setThreadTitle(inboxThread.ThreadTitle);
    $("#messages").html(threadView);
    attachNewMessageEvents(inboxThread);
    NotificationReceiver.observeThread({
        threadId: inboxThread.ThreadId,
        threadNewMessage: receivedNewMessageOnThread,
        threadMessagesRead: markMessagesAsRead
    });
}

function attachNewMessageEvents(inboxThread) {
    currentInboxThread = inboxThread;

    const sendButton = $("#messageTextSend");
    sendButton.off("click");
    sendButton.on("click", () => {
        const messageInput = $("#messageTextInput");
        const messageText = messageInput.val();
        messageInput.val("");

        if (!currentInboxThread.ThreadId) {
            sendButton.prop("disabled", true);
        }

        const newMessage = NewMessageFactory.createMessage(messageText);
        addMessageToThread(newMessage);

        MessageThreadService
            .sendMessage({
                inboxThread: currentInboxThread,
                message: newMessage
            })
            .then(sendMessageResponse => {
                if (!currentInboxThread.ThreadId) {
                    Routes.goTo(`${Routes.messageInbox}?threadid=${sendMessageResponse.inboxThread.ThreadId}`);
                }
                currentInboxThread = sendMessageResponse.inboxThread;
                replaceMessageOnThread(sendMessageResponse.message);
            });
    });
}

function setThreadTitle(title) {
    $("#threadTitle h4").text(title);
}

function replaceMessageOnThread(message) {
    const sentMessageView = MessagePartial.getView(message);
    $(`[data-stamp='${message.Stamp}']`).replaceWith(sentMessageView);
}

function addMessageToThread(message) {
    if ($(`[data-stamp='${message.Stamp}']`).length) return;

    const messageView = MessagePartial.getView(message);
    $("#messages ul").prepend(messageView);
}

function receivedNewMessageOnThread(message) {
    if (currentInboxThread.ThreadId !== message.ThreadId) return;

    addMessageToThread(message);
}

function markMessagesAsRead(messagesRead) {
    messagesRead.ReadMessageStamps.forEach(messageStamp => {
        $(`[data-stamp='${messageStamp}'] [data-message-status]`).html("*Has been read: Read");
    });
}

function checkUnauthorised(error) {
    if (error && error.status && error.status === 401) {
        PostLoginRedirect.storeRedirectUrl(window.location.href);
        Routes.goTo(Routes.login);
    }
}
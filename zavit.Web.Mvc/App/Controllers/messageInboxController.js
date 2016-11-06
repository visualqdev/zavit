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
            const threadView = MessageThreadPartial.getView(inboxThread);
            setThreadTitle(inboxThread.ThreadTitle);
            $("#messages").html(threadView);
            attachNewMessageEvents(inboxThread);
            NotificationReceiver.observeThread(inboxThread.ThreadId, addMessageToThread);
        })
        .then(Progress.done)
        .then(MessageLayout.setUp);
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
                const threadView = MessageThreadPartial.getView(inboxThread);
                setThreadTitle(inboxThread.ThreadTitle);
                $("#messages").html(threadView);
                attachNewMessageEvents(inboxThread);
            });
    });
}

function attachNewMessageEvents(inboxThread) {
    let currentInboxThread = inboxThread;

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
                const sentMessageView = MessagePartial.getView(sendMessageResponse.message);
                $(`[data-stamp='${sendMessageResponse.message.Stamp}']`).replaceWith(sentMessageView);
            });
    });
}

function setThreadTitle(title) {
    $("#threadTitle h4").text(title);
}

function addMessageToThread(message) {
    const messageView = MessagePartial.getView(message);
    $("#messages ul").prepend(messageView);
}
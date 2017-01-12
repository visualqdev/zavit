import * as MainContent from "../layout/mainContent";
import * as Routes from "../routing/routes";
import * as Progress from "../modules/loading/progress";
import * as IndexView from "../views/messageInbox/indexView";
import * as MessagePartial from "../views/messageInbox/messagePartial";
import * as MessageThreadListPartial from "../views/messageInbox/messageThreadListPartial";
import * as MessageThreadPartial from "../views/messageInbox/messageThreadPartial";
import * as MessageThreadService from "../modules/messaging/messageThreadService";
import * as MessageInboxService from "../modules/messaging/messageInboxService";
import * as NewMessageFactory from "../modules/messaging/newMessageFactory";
import * as MessageRecipientSearchModal from "../modules/messaging/messageRecipientSearchModal";
import * as MessageLayout from "../modules/messaging/messageLayout";
import * as NotificationReceiver from "../modules/notifications/notificationReceiver";
import * as PostLoginRedirect from "../modules/account/postLoginRedirect";
import { htmlEncode } from "../modules/htmlUtils/htmlEncoder";

let currentInboxThread;
const messageInboxObserverId = "messageInboxObserver";

export function index(options) {
    MainContent.load(Routes.messageInbox);
    Progress.start();

    MessageInboxService
        .getInboxThreads()
        .then(messageThreads => {
            const view = IndexView.getView(messageThreads);
            MainContent.append(view);

            attachInboxEvents();
            return MessageThreadService.getInboxThread(options, messageThreads);
        })
        .then(inboxThread => {
            
            const threadView = MessageThreadPartial.getView(inboxThread);
            setThreadTitle(inboxThread.ThreadTitle);
            $("#messages").html(threadView);
            attachNewMessageEvents(inboxThread);
            showInboxThread(inboxThread);
            MessageLayout.setUp(inboxThread.ThreadId);
        })
        .catch((error) => {
            checkUnauthorised(error);
        })
        .then(Progress.done);
}

function attachInboxEvents() {
    $("#messageThreadsContainer").on("click", "[data-thread-id]", function(e) {
        e.preventDefault();
        e.stopPropagation();
        const threadId = $(this).attr("data-thread-id");
        MessageThreadService.getInboxThread({
                threadId
            })
            .then(inboxThread => {
                const threadView = MessageThreadPartial.getView(inboxThread);

                setThreadTitle(inboxThread.ThreadTitle);
                
                $("#messages").html(threadView);
                attachNewMessageEvents(inboxThread);
                showInboxThread(inboxThread);
                MessageLayout.threadSelected(this);
                
                MessageLayout.adjustHeightOfMainContainer($("#messages"));
            });
    });

    function removeInboxClass() {
        $('#arrangeNew').removeClass('returnToInbox');
    }

    $("#mainContent").delegate(".threadSelected #arrangeNew", "click", (e) => {
        e.preventDefault();
        $("#messageThreads").removeClass("threadSelected");
        $('#arrangeNew').html('<i class="fa fa-plus-circle" aria-hidden="true"></i>Arrange new');
        setTimeout(removeInboxClass, 100);
    });

    $("#mainContent").delegate("#arrangeNew", "click", (e) => {
        e.preventDefault();
        if (!$(e.currentTarget).hasClass('returnToInbox')) {
            MessageRecipientSearchModal.show();
        }
    });

    NotificationReceiver.observeInbox(messageInboxObserverId, messageInboxHasChanged);
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
    $("#threadTitle h4").text(htmlEncode(title));
}

function replaceMessageOnThread(message) {
    const sentMessageView = MessagePartial.getView(message);
    $(`[data-stamp='${message.Stamp}']`).replaceWith(sentMessageView);
    MessageLayout.setScrollPositionToBottom();
}

function addMessageToThread(message) {
    if ($(`[data-stamp='${message.Stamp}']`).length) return;

    const messageView = MessagePartial.getView(message);
    $("#messages ul").append(messageView);
}

function receivedNewMessageOnThread(message) {
    if (currentInboxThread.ThreadId !== message.ThreadId) return;

    addMessageToThread(message);
    MessageThreadService.confirmMessageRead(message.Stamp);
}

function markMessagesAsRead(messagesRead) {
    messagesRead.ReadMessageStamps.forEach(messageStamp => {
        var indicator = $(`[data-stamp='${messageStamp}'] span.sent`);
        indicator.removeClass("sent");
        indicator.addClass("read");
    });
}

function checkUnauthorised(error) {
    if (error && error.status && error.status === 401) {
        PostLoginRedirect.storeRedirectUrl(window.location.href);
        Routes.goTo(Routes.login);
    }
}

function messageInboxHasChanged() {
    MessageInboxService
        .getInboxThreads()
        .then(messageThreads => {
            var currentlySelectedThread = MessageLayout.currentlySelectedThreadId();

            const threadView = MessageThreadListPartial.getView(messageThreads);
            $("#messageThreadsContainer").html(threadView);
            MessageLayout.selectThreadId(currentlySelectedThread);
        });
}
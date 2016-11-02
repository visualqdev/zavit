import * as MainContent from "../layout/mainContent";
import * as Routes from "../routing/routes";
import * as Progress from "../modules/loading/progress";
import * as IndexView from "../views/messageInbox/indexView";
import * as MessageThreadPartial from "../views/messageInbox/messageThreadPartial";
import * as MessageThreadService from "../modules/messaging/messageThreadService";
import * as MessageInboxService from "../modules/messaging/messageInboxService";
import * as NewMessageFactory from "../modules/messaging/newMessageFactory";

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
            $("#messageThreadMessages").html(threadView);
            attachNewMessageEvents(inboxThread);
        })
        .then(Progress.done);
}

function attachInboxEvents() {
    $("#messageThreads").on("click", "[data-thread-id]", (e) => {
        e.preventDefault();
        const threadId = $(e.target).attr("data-thread-id");
        MessageThreadService.getInboxThread({
                threadId
            })
            .then(inboxThread => {
                const threadView = MessageThreadPartial.getView(inboxThread);
                $("#messageThreadMessages").html(threadView);
                attachNewMessageEvents(inboxThread);
            });
    });
}

function attachNewMessageEvents(inboxThread) {
    let currentInboxThread = inboxThread;

    $("#messageTextSend").on("click", () => {
        const messageText = $("#messageTextInput").val();
        const sendButton = $(this);

        if (!currentInboxThread.ThreadId) {
            sendButton.prop("disabled", true);
        }

        const newMessage = NewMessageFactory.createMessage(messageText);

        MessageThreadService
            .sendMessage({
                inboxThread: currentInboxThread,
                message: newMessage
            })
            .then(sendMessageRespone => {
                if (!currentInboxThread.ThreadId) {
                    Routes.goTo(`${Routes.messageInbox}?threadid=${sendMessageRespone.inboxThread.ThreadId}`);
                }
                currentInboxThread = sendMessageRespone.inboxThread;
                alert(sendMessageRespone.message.Body);
            });
    });
}
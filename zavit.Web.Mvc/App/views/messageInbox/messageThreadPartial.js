import * as MessagePartial from "./messagePartial";

export function getView(messageThread) {
    return `            
        <ul>
            ${getMessages(messageThread)}
        </ul>            
        `;
}

function getMessages(messageThread) {
    let messagesMarkup = "";

    if (messageThread) {
        messageThread.MessagesCollection.Messages.forEach(message => {
            messagesMarkup = MessagePartial.getView(message) + messagesMarkup;
        });
    }

    return messagesMarkup;
}
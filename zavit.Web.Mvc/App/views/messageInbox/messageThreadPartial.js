import * as MessagePartial from "./messagePartial";

export function getView(messageThread) {
    return `            
        <ul>
            ${getMessages(messageThread.MessagesCollection.Messages)}
        </ul>            
        `;
}

function getMessages(messages) {
    let messagesMarkup = "";

    messages.forEach(message => {
        messagesMarkup = MessagePartial.getView(message) + messagesMarkup;
    });

    return messagesMarkup;
}
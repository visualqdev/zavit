import * as MessagePartial from "./messagePartial";

export function getView(messageThread) {
    return `
        <div>
            <h3>${messageThread.ThreadTitle}</h3>
            <ul>
                ${getMessages(messageThread.MessagesCollection.Messages)}
            </ul>
            <div>
                <input id="messageTextInput" type="text"/>
                <input id="messageTextSend" type="button" value="Send" />
            </div>
        </div>          
        `;
}

function getMessages(messages) {
    let messagesMarkup = "";

    messages.forEach(message => {
        messagesMarkup += MessagePartial.getView(message);
    });

    return messagesMarkup;
}
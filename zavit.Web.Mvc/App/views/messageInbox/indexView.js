import * as Routes from "../../routing/routes";

export function getView(messageThreads) {
    return `
        <div id="messageThreads">
            <h3>MessageThreads</h3>
            <ul>
                ${getMessageThreads(messageThreads)}
            </ul>
        </div>
        <div id="messageThreadMessages">
        </div>        
        `;
}

function getMessageThreads(messageThreads) {
    let messageThreadsMarkup = "";

    messageThreads.forEach(messageThread => {
        messageThreadsMarkup += `
            <li>
                <a href="/#/${Routes.messageInbox}?threadid=${messageThread.ThreadId}" data-thread-id="${messageThread.ThreadId}">
                    ${messageThread.ThreadTitle}
                </a>
            </li>
            `;
    });

    return messageThreadsMarkup;
}
import * as Routes from "../../routing/routes";

export function getView(messageThreads) {
    let messageThreadsMarkup = `<ul id="messageThreads">`;

    messageThreads.forEach(messageThread => {
        messageThreadsMarkup += `
            <li>
                <a href="/#/${Routes.messageInbox}?threadid=${messageThread.ThreadId}" data-thread-id="${messageThread.ThreadId}">
                    ${messageThread.ThreadTitle}
                </a>
            </li>
            `;
    });

    messageThreadsMarkup += `</ul>`;

    return messageThreadsMarkup;
}
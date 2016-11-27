import * as Routes from "../../routing/routes";

export function getView(messageThreads) {
    let messageThreadsMarkup = `<ul id="messageThreadList">`;

    messageThreads.forEach(messageThread => {
        messageThreadsMarkup += `
            <li>                
                <div class="inboxThread" data-thread-id="${messageThread.ThreadId}">
                    <div class="inboxThreadInfo">
                        <div class="inboxThreadInfoRow">
                            <span class="inboxThreadTitle pull-left">${messageThread.ThreadTitle}</span>
                            <span class="inboxThreadDate pull-right">${moment(messageThread.LatestMessageSentOn).calendar()}</span>
                        </div>
                        <div class="inboxThreadInfoRow">
                            <span class="inboxThreadLatestMessage pull-left">${messageThread.LatestMessageBody}</span>
                            ${getUnreadCount(messageThread)}                            
                        </div>
                    </div>                    
                </div>
            </li>
            `;
    });

    messageThreadsMarkup += `</ul>`;

    return messageThreadsMarkup;
}

function getUnreadCount(messageThread) {
    if (messageThread.UnreadMessageCount < 1) return "";

    return `
        <span class="inboxThreadUnreadCount pull-right">${messageThread.UnreadMessageCount}</span>
        `;
}
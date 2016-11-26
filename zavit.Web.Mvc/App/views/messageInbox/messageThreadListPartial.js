import * as Routes from "../../routing/routes";

export function getView(messageThreads) {
    let messageThreadsMarkup = `<ul id="messageThreadList">`;

    messageThreads.forEach(messageThread => {
        messageThreadsMarkup += `
            <li>                
                <div class="inboxThread" data-thread-id="${messageThread.ThreadId}">
                    <div class="inboxThreadInfo">
                        <div class="inboxThreadInfoRow inboxThreadInfoHeading">
                            <span class="inboxThreadTitle pull-left">${messageThread.ThreadTitle}</span>
                            <span class="inboxThreadDate pull-right">${moment(messageThread.LatestMessageSentOn).calendar()}</span>
                        </div>
                        <div class="inboxThreadInfoRow">
                            <span class="inboxThreadLatestMessage">${messageThread.LatestMessageBody}</span>
                        </div>
                    </div>                    
                </div>
            </li>
            `;
    });

    messageThreadsMarkup += `</ul>`;

    return messageThreadsMarkup;
}
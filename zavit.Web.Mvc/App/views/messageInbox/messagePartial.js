import * as AccountService from "../../modules/account/accountService";

export function getView(message) {
    return `
        <li data-stamp="${message.Stamp}" class="messageContainer ${userType(message)}">
            ${displayName(message)}
            <span class="body">${message.Body}</span>
            ${messageHasBeenRead(message.Status)}
            <span class="date" data-message-status>${moment(message.SentOn).fromNow()}</span>
        </li>
        `;
}

function currentUserId() {
    return AccountService.currentUserAccount().id;
}

function userType(message) {
    const userIsCurrentUser = message.Sender.AccountId === currentUserId();

    if (userIsCurrentUser) return "currentUser";
    return "";
}

function messageHasBeenRead(status) {
    return `<span class="tick ${addClassForHasBeenRead(status)}">&#10004;</span>`;
}

function addClassForHasBeenRead(status) {
    return status.toLowerCase();
}
function displayName(message) {
    const userIsNotCurrentUser = message.Sender.AccountId !== currentUserId();

    if(userIsNotCurrentUser) return `<span class="displayName">${message.Sender.DisplayName}</span>`;
    return "";
}
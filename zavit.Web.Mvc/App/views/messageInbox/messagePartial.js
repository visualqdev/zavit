import * as AccountService from "../../modules/account/accountService";

export function getView(message) {
    return `
        <li data-stamp="${message.Stamp}" class="messageContainer ${userType(message)}">
            ${displayName(message)}
            <span class="body">${message.Body}</span>
            ${messageHasBeenRead(message)}
            <span class="date" data-message-status>${moment(message.SentOn).calendar()}</span>
        </li>
        `;
}

function currentUserId() {
    return AccountService.currentUserAccount().accountId;
}

function userType(message) {
    if (!message.Sender) return false;
    const userIsCurrentUser = message.Sender.AccountId === parseInt(currentUserId());
    if (userIsCurrentUser) return "currentUser";
    return "";
}

function messageHasBeenRead(message) {
    if (!message.Sender) return false;
    const userIsNotCurrentUser = message.Sender.AccountId !== parseInt(currentUserId());
    if (userIsNotCurrentUser) return "";
    return `<span class="tick ${addClassForHasBeenRead(message.Status)}">&#10004;</span>`;
}

function addClassForHasBeenRead(status) {
    return status.toLowerCase();
}

function displayName(message) {
    if (!message.Sender) return false;
    const userIsNotCurrentUser = message.Sender.AccountId !== parseInt(currentUserId());

    if(userIsNotCurrentUser) return `<span class="displayName">${message.Sender.DisplayName}</span>`;
    return "";
}
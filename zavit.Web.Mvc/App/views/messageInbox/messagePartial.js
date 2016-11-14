import * as AccountService from "../../modules/account/accountService";

export function getView(message) {
    return `
        <li class="messageContainer ${userType(message)}">
            ${displayName(message)}
            <span class="body">${message.Body}</span>
            ${messageHasBeenRead(message.HasBeenRead)}
            <span class="date">${moment(message.SentOn).fromNow()}</span>
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

function messageHasBeenRead(hasBeenRead) {
    return `<span class="tick ${addClassForHasBeenRead(hasBeenRead)}">&#10004;</span>`;
}

function addClassForHasBeenRead(hasBeenRead) {
    if (hasBeenRead) return "read";
    return "";
}
function displayName(message) {
    const userIsNotCurrentUser = message.Sender.AccountId !== currentUserId();

    if(userIsNotCurrentUser) return `<span class="displayName">${message.Sender.DisplayName}</span>`;
    return "";
}
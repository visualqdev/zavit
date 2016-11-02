export function getView(message) {
    return `
        <li data-stamp="${message.Stamp}">
            <span>*${message.Sender ? message.Sender.DisplayName : ""}</span>
            <span>*${message.SentOn ? message.SentOn : ""}</span>
            <span>*${message.Body}</span>
            <span>*Has been read: ${message.Status}</span>
        </li>
        `;
}
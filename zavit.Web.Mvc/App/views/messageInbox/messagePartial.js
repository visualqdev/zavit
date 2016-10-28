export function getView(message) {
    return `
        <li>
            <span>*${message.Sender.DisplayName}</span>
            <span>*${message.SentOn}</span>
            <span>*${message.Body}</span>
            <span>*Has been read: ${message.HasBeenRead}</span>
        </li>
        `;
}
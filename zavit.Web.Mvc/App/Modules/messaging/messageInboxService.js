import * as MessageThreadClient from "./messageThreadClient";

export function getInboxThreads() {
    return MessageThreadClient.getMessageThreads();
}
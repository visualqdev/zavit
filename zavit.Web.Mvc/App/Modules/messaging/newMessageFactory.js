import * as Guid from "../uniqueIdentifier/guid";

export function createMessage(messageBody) {
    const guid = Guid.newGuid();

    return {
        Body: messageBody,
        Stamp: guid,
        Status: "Sending"
    };
}
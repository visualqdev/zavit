import * as Guid from "../uniqueIdentifier/guid";
import * as AccountService from "../account/accountService";

export function createMessage(messageBody) {
    const guid = Guid.newGuid();
    const currentUserAccount = AccountService.currentUserAccount();

    return {
        Body: messageBody,
        Stamp: guid,
        Status: "Sending",
        Sender: {
            AccountId: parseInt(currentUserAccount.accountId)
        }
    };
}
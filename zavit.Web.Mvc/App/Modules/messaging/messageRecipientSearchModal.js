import * as MessageRecipientSearchModalView from "../../views/messageRecipientSearch/messageRecipientSearchModalView";
import * as MessageRecipientsListPartial from "../../views/messageRecipientSearch/messageRecipientsListPartial";
import * as MessageRecipientsClient from "./messageRecipientsClient";
import * as MessageRecipientSearchLayout from "./messageRecipientSearchLayout";

const recipientSuggestionTake = 20;
let selectedRecipients = [];
let suggestedRecipients = [];

export function show(options) {
    const view = MessageRecipientSearchModalView.getView();
    const modalView = $(view);

    modalView.on("hidden.bs.modal", () => {
        modalView.remove();
    });

    modalView.on("shown.bs.modal", () => {
        attachSearchEvents();
        MessageRecipientSearchLayout.focusSearchInput();
    });

    modalView.modal("show");
}

function searchRecipients() {
    const searchTerm = MessageRecipientSearchLayout.getSearchTerm();

    if (searchTerm.length > 0) {
        MessageRecipientsClient
            .suggestRecipients(searchTerm, 0, recipientSuggestionTake)
            .then(recipientsCollection => {
                suggestedRecipients = recipientsCollection.Recipients;
                const recipientsView = MessageRecipientsListPartial.getView({
                    SuggestedRecipients: recipientsCollection.Recipients
                });

                MessageRecipientSearchLayout.replaceListView(recipientsView);
                MessageRecipientSearchLayout.selectRecipients(selectedRecipients.map(recipient => recipient.AccountId));
            });
    } else {
        const recipientsView = MessageRecipientsListPartial.getView({
            SuggestedRecipients: selectedRecipients
        });

        MessageRecipientSearchLayout.replaceListView(recipientsView);
        MessageRecipientSearchLayout.selectRecipients(selectedRecipients.map(recipient => recipient.AccountId));
    }
}

function attachSearchEvents() {
    let delayTimer;
    
    MessageRecipientSearchLayout.onSearchInputChange(() => {
        clearTimeout(delayTimer);
        delayTimer = setTimeout(() => {
            searchRecipients();
        }, 300);
    });

    MessageRecipientSearchLayout.onRecipientSelected({
        onSelected: selectedRecipientId => {
            const recipient = suggestedRecipients.find(element => {
                return element.AccountId == selectedRecipientId;
            });
            selectedRecipients.push(recipient);
            searchRecipients();
        },
        onDeselected: deselectedRecipientId => {
            const index = selectedRecipients.findIndex(element => element.AccountId == deselectedRecipientId);

            if (index > -1) {
                selectedRecipients.splice(index, 1);
            }
            searchRecipients();
        }
    });
}
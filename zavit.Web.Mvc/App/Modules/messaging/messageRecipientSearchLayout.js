export function getSearchTerm() {
    return $("#searchRecipientsModal .recipientSuggestion input").val();
}

export function replaceListView(listView) {
    $("#searchRecipientsModal .recipientResults").html(listView);
}

export function onSearchInputChange(callback) {
    $("#searchRecipientsModal .recipientSuggestion input").keyup(callback);
}

export function focusSearchInput() {
    $("#searchRecipientsModal .recipientSuggestion input").focus();
}

export function clearSearchInput() {
    $("#searchRecipientsModal .recipientSuggestion input").val("");
}

export function onRecipientSelected(callbacks) {
    $("#searchRecipientsModal").on("click", ".recipientResult", function(e) {
        e.preventDefault();
        e.stopPropagation();

        const recipientContainer = $(this),
            recipientId = recipientContainer.attr("data-id");
        
        if (recipientContainer.hasClass("selected")) {
            recipientContainer.removeClass("selected");
            clearSearchInput();
            callbacks.onDeselected(recipientId);
            focusSearchInput();
        } else {
            recipientContainer.addClass("selected");
            clearSearchInput();
            callbacks.onSelected(recipientId);
            focusSearchInput();
        }
    });
}

export function selectRecipients(recipientIds) {
    recipientIds.forEach(recipientId => {
        const recipientContainer = $(`#searchRecipientsModal .recipientResult[data-id=${recipientId}]`);
        if (recipientContainer.length && !recipientContainer.hasClass("selected")) {
            recipientContainer.addClass("selected");
        }
    });
}
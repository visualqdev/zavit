export function getView(options) {
    let recipientsMarkup = "";

    options.SuggestedRecipients.forEach(recipient =>
        recipientsMarkup += `
            <div class="col-sm-12 recipientResult" data-id="${recipient.AccountId}">
                <div class="memberImage"><div><i class="fa fa-user" aria-hidden="true"></i></div></div>                
                <div class="memberDetails">
                    <div class="content">
                        <h3>${recipient.DisplayName}</h3>
                    </div>
                </div>
            </div>`);

    return recipientsMarkup;
}
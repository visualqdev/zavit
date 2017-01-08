import { htmlEncode } from "../../modules/htmlUtils/htmlEncoder";

export function getView(options) {
    let recipientsMarkup = "";

    options.SuggestedRecipients.forEach(recipient =>
        recipientsMarkup += `
            <div class="col-sm-12 recipientResult" data-id="${recipient.AccountId}">
                <div class="memberImage">
                    ${recipientImage(recipient)}
                </div>
                <div class="memberDetails">
                    <div class="content">
                        <h3>${htmlEncode(recipient.DisplayName)}</h3>
                    </div>
                </div>
            </div>`);

    return recipientsMarkup;
}

function recipientImage(recipient) {
    if (recipient.ProfileImageUrl == null) {
        return `
        <div class="memberImage"><div><i class="fa fa-user" aria-hidden="true"></i></div></div>`;
    }

    return`
        <div class="profileImageHolder" style='background: url("${recipient.ProfileImageUrl}") 50% 50%/cover no-repeat;'></div>`;
    
}
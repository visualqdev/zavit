export function getView(options) {
    return `
        <div class='modal fade' id='searchRecipientsModal' tabindex='-1' role='dialog'>
            <div class='modal-dialog'>
                <div class='searhRecipientsContainer'>
                    <h2>Find Recipients</h2>
                    <div class="recipientSuggestion">
                        <input type="text" placeholder="Start typing"/>
                    </div>
                    <div class="recipientResults">                                                               
                    </div>
                    <div>
                        <input value="Done" class="btn btn-primary doneButton" type="button"/>
                    </div>
                </div>
            </div>
        </div>`;
}
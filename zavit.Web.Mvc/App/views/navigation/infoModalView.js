export function getView() {
    return `
        <div class='modal fade' id='infoModal' tabindex='-1' role='dialog'>
            <div class='modal-dialog'>
                <div class='infoModalContainer'>
                    <h2>Welcome to zavit</h2>
                    <p>zavit can help you find new people to share your sporting activities with</p>
                    <ol>
                        <li>Find your venue of choice</li>
                        <li>Either find a member who shares your activity or make yourself available</li>
                        <li>Arrange and enjoy!</li>
                    </ol>
                    <div class='loginHelp'>
                        <a href='#' id='loginRegisterLink'data-dismiss="modal">OK</a>
                    </div>
                </div>
            </div>

        </div>`;
}
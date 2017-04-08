import * as MessageThreadListPartial from "./messageThreadListPartial";

export function getView(messageThreads) {
    return `
        <div id="messageThreads" class="container">
            <div class="row">
                <div class="col-md-4">
                    <a href="#" id="arrangeNew"><i class="fa fa-plus-circle" aria-hidden="true"></i>Arrange new</a>
                    <a href="#" id="backToInbox" style="display:none;"><i class="fa fa-chevron-left returnToInbox" aria-hidden="true"></i>Back to inbox</a>
                </div>
                <div class="col-md-8 noColPaddingLeft" id="threadTitle"> <h4></h4></div>
            </div>
            <div class="row" id="messagesContainer">
                <div id="enableScroll">
                    <div class="col-md-4" id="messageThreadsContainer">
                        ${MessageThreadListPartial.getView(messageThreads)}
                    </div>
                    <div class="col-md-8 noColPaddingLeft">
                        <div id="messages"></div>
                        <div id="controls">
                            <input id="messageTextInput" placeholder="Type your message here"  type="text"/>
                            <input id="messageTextSend" type="button" class="btn btn-primary" value="Send" />
                        </div>
                    </div>  
                </div>
            </div>
        </div>
        `;
}
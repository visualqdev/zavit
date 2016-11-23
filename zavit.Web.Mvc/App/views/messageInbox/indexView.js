import * as MessageThreadListPartial from "./messageThreadListPartial";

export function getView(messageThreads) {
    return `
        <div id="messageThreads" class="container">
            <div id="messagesHeading">
                <h2 id="mainHeading"><i class="fa fa-commenting-o" aria-hidden="true"></i>Messages </h2>
            </div>
            <div class="row">
                <div class="col-md-3"><a href="#" id="arrangeNew"><i class="fa fa-plus-circle" aria-hidden="true"></i>Arrange new</a></div>
                <div class="col-md-9 noColPaddingLeft" id="threadTitle"> <h4></h4></div>
            </div>
            <div class="row" id="messagesContainer">
                <div id="enableScroll">
                    <div class="col-md-3" id="messageThreadsContainer">
                        ${MessageThreadListPartial.getView(messageThreads)}
                    </div>
                    <div class="col-md-9 noColPaddingLeft">
                        <div id="messages"></div>
                        <div id="controls">
                            <input id="messageTextInput" type="text"/>
                            <input id="messageTextSend" type="button" value="Send" />
                        </div>
                    </div>  
                </div>
            </div>
        </div>
        `;
}
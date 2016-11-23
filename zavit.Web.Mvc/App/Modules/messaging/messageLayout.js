export function setUp(threadId) {
    const $messagesContainer = $("#messages");
    selectThreadId(threadId);
    setWindowResizeWatch($messagesContainer);
    adjustHeightOfMainContainer($messagesContainer);
    adjustCssPositioningForMessagesContainer($messagesContainer);
    adjustHeightOfMessageThreadColumn();
    setMediaQueryWatch();
}

export function currentlySelectedThreadId() {
    const link = $("#messageThreads li a.selected");
    if (link.length)
        return link.attr("data-thread-id");
}

export function selectThreadId(threadId) {
    threadSelected(`#messageThreads a[data-thread-id=${threadId}]`);
}

export function threadSelected(selectedThread) {
    $("#messageThreads li a.selected").removeClass("selected");
    $(selectedThread).addClass("selected");
    $("#messageThreads").addClass("threadSelected");
    if (window.matchMedia("(max-width: 990px)").matches) {
        $("#arrangeNew").html("<i class='fa fa-chevron-left' aria-hidden='true'></i>Back to inbox");
    }
    adjustCssPositioningForMessagesContainer($('#messages'));
}

export function adjustHeightOfMainContainer($messagesContainer) {

    const topOfMessages = $messagesContainer.offset().top,
        heightOfControls = $("#controls").height(),
        heightOfMargin = parseInt($("#controls").css("margin-top")),
        heightOfFooter = $(".footer").height(),
        heightOfMessagesContainer = $(window).height() - topOfMessages - heightOfControls - heightOfFooter - heightOfMargin;

    $messagesContainer.height(heightOfMessagesContainer);
}

function changeToRelativePositioning($element) {
    $element.css("position", "relative");
}

function changeToAbsolutePositioning($element) {
    $element.css("position", "absolute");
}

function setToScrollPosition($element, height) {
    $element.scrollTop(height);
}

function heightOfMessages() {

    let combinedMessagesHeight = 0;
    const $messages = $("#messages ul li");

    function calculateMessageHeight(message) {
        combinedMessagesHeight += message.offsetHeight;
    }

    $.each($messages, (index, $message) => calculateMessageHeight($message));

    return combinedMessagesHeight;
}

export function setScrollPositionToBottom(){
    setToScrollPosition($("#messages"), heightOfMessages());
}

export function adjustCssPositioningForMessagesContainer($messagesContainer) {
    const $messagesList = $("#messages ul");
    if (heightOfMessages() > $messagesContainer.height()) {
        changeToRelativePositioning($messagesList);
        setToScrollPosition($messagesContainer, heightOfMessages());
    } 
    else {
        changeToAbsolutePositioning($messagesList);
    }
}

function adjustHeightOfMessageThreadColumn() {
    if(!window.matchMedia("(max-width: 990px)").matches)  $("#messageThreadsContainer").css("max-height", $("#controls").position().top + $("#controls").height() - parseInt($("#controls").css("margin-top")));

}

function setWindowResizeWatch($messagesContainer) {
    $(window).resize(() => {
        adjustHeightOfMainContainer($messagesContainer);
        adjustCssPositioningForMessagesContainer($messagesContainer);
        adjustHeightOfMessageThreadColumn();
    });
}

function setMediaQueryWatch() {

    const mql = window.matchMedia("(max-width: 990px)");

    const handleMediaChange = function (mediaQueryList) {
        if (mediaQueryList.matches) {
            $(".threadSelected #arrangeNew").html("<i class='fa fa-chevron-left' aria-hidden='true'></i>Back to inbox");
            
        } else {
            $("#arrangeNew").html("<i class='fa fa-plus-circle' aria-hidden='true'></i>Arrange new");
        }
    }

    mql.addListener(handleMediaChange);
    handleMediaChange(mql);
}
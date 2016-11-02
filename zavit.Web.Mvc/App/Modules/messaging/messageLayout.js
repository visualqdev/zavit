export function selectFirstNavItem() {
    $("#messageThreads li:first a").addClass("selected");
}

export function styleSelected(e) {
    $(e.target).addClass("selected");
    $(e.target).closest("li").siblings().children().removeClass("selected");
}

function adjustHeightOfMainContainer($messagesContainer) {

    const topOfMessages = $messagesContainer.offset().top,
        heightOfControls = $("#controls").height(),
        heightOfFooter = $(".footer").height(),
        heightOfMessagesContainer = $(window).height() - topOfMessages - heightOfControls - heightOfFooter;

    $messagesContainer.height(heightOfMessagesContainer);
}

function setWindowResizeWatch($messagesContainer) {
    $(window).resize(function () {
        adjustHeightOfMainContainer($messagesContainer);
    });
}

export function setUp() {
    const $messagesContainer = $("#messages");
    selectFirstNavItem();
    setWindowResizeWatch($messagesContainer);
    adjustHeightOfMainContainer($messagesContainer);
}
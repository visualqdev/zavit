﻿import { htmlEncode } from "../htmlUtils/htmlEncoder";

export function setUp(options) {
    const $messagesContainer = $("#messages");
    selectThreadId(options.selectedThreadId);
    setWindowResizeWatch($messagesContainer);
    adjustHeightOfMainContainer($messagesContainer);
    adjustCssPositioningForMessagesContainer($messagesContainer);
    adjustHeightOfMessageThreadColumn();
    setMediaQueryWatch();
    adjustInboxThreadListLayout();
    attachEvents(options);
}

export function currentlySelectedThreadId() {
    const link = $("#messageThreadList li .inboxThread.selected");
    if (link.length)
        return link.attr("data-thread-id");
}

export function selectThreadId(threadId) {
    threadSelected(`#messageThreadList [data-thread-id=${threadId}]`);
}

export function threadSelected(selectedThread) {
    $("#messageThreadList li .selected[data-thread-id]").removeClass("selected");
    $(selectedThread).addClass("selected");
    $("#messageThreads").addClass("threadSelected");
    if (window.matchMedia("(max-width: 990px)").matches) {
        showBackToInbox();
    }
    adjustCssPositioningForMessagesContainer($('#messages'));
    adjustInboxThreadListLayout();
    fadeOutUnreadCount(selectedThread);
}

export function adjustHeightOfMainContainer($messagesContainer) {

    const topOfMessages = $messagesContainer.offset().top,
        heightOfControls = $("#controls").height(),
        heightOfMargin = parseInt($("#controls").css("margin-top")),
        heightOfContainerMargin = parseInt($("#messagesContainer").css("margin-top")),
        heightOfFooter = $(".footer").height();
        let heightOfMessagesContainer = $(window).height() - topOfMessages - heightOfControls - heightOfFooter - heightOfMargin - heightOfContainerMargin;

        if (window.matchMedia("(max-width: 990px)").matches) heightOfMessagesContainer = heightOfMessagesContainer - parseInt($("#arrangeNew").css("margin-top"));

    $messagesContainer.height(heightOfMessagesContainer);
}

function fadeOutUnreadCount(selectedThread) {
    $(selectedThread).find(".inboxThreadUnreadCount").fadeOut("slow");
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
    if(!window.matchMedia("(max-width: 990px)").matches)  $("#messageThreadsContainer").css("max-height", $("#controls").position().top + $("#controls").height() + parseInt($("#controls").css("margin-top") + parseInt($("#controls").css("margin-bottom"))));

}

function adjustInboxThreadListLayout() {
    $(".inboxThreadInfo").each(function(index, container) {

        const jContainer = $(container);
        const containerWidth = jContainer.width();
        const dateWidth = jContainer.find(".inboxThreadDate").width();
        jContainer.find(".inboxThreadTitle").css("max-width", containerWidth - dateWidth);

        const unreadCount = jContainer.find(".inboxThreadUnreadCount");
        if (unreadCount.length) {
            jContainer.find(".inboxThreadLatestMessage").css("max-width", containerWidth - unreadCount.width());
        }
    });
}

function setWindowResizeWatch($messagesContainer) {
    $(window).resize(() => {
        adjustHeightOfMainContainer($messagesContainer);
        adjustCssPositioningForMessagesContainer($messagesContainer);
        adjustHeightOfMessageThreadColumn();
        adjustInboxThreadListLayout();
    });
}

function setMediaQueryWatch() {

    const mql = window.matchMedia("(max-width: 990px)");

    const handleMediaChange = function (mediaQueryList) {
        if (mediaQueryList.matches) {
            showBackToInbox();

        } else {
            hideBackToInbox();
        }
    }

    mql.addListener(handleMediaChange);
    handleMediaChange(mql);
}

function showBackToInbox() {
    $("#arrangeNew").hide();
    $("#backToInbox").show();
}

function hideBackToInbox() {
    $("#backToInbox").hide();
    $("#arrangeNew").show();
}

export function setMessageThreadView(threadHtml) {
    $("#messages").html(threadHtml);
}

export function setThreadTitle(title) {
    $("#threadTitle h4").text(htmlEncode(title));
}

function attachEvents(options) {
    $("#messageThreads").delegate("#arrangeNew", "click", (e) => {
        e.preventDefault();
        options.onArrangeNew();
    });

    $("#messageThreads").delegate("#backToInbox", "click", (e) => {
        e.preventDefault();
        hideBackToInbox();
        $("#messageThreads").removeClass("threadSelected");
    });

    $("#messageThreadsContainer").on("click", "[data-thread-id]", function(e) {
        e.preventDefault();
        e.stopPropagation();

        const threadId = $(this).attr("data-thread-id");

        options.onThreadSelected(threadId);
        threadSelected(this);
        adjustHeightOfMainContainer($("#messages"));
    });
}

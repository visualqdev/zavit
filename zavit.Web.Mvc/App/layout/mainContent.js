export function load(pageName, content) {
    if (!content)
        content = "";

    $("[data-redirect-remove]").remove();

    const mainContent = $(`<div id="mainContent" class="row content" data-page="${pageName}">${content}</div>`);
    $("#mainContent").replaceWith(mainContent);

    pageName !== "" ? $('.search').hide() : $('.search').show();
}

export function isOnPage(pageName) {
    const page = $(`#mainContent[data-page='${pageName}']`);
    if (page.length)
        return true;

    return false;
}

export function append(content) {
    $("#mainContent").append(content);
}
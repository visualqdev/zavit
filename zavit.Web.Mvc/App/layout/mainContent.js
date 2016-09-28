export function load(content) {
    if (!content)
        content = "";

    $("[data-redirect-remove]").remove();
    const mainContent = $(`<div id="mainContent" class="row content">${content}</div>`);
    $("#mainContent").replaceWith(mainContent);
}

export function append(content) {
    $("#mainContent").append(content);
}
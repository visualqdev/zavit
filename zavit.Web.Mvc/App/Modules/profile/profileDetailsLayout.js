export function attachEvents() {
    $("#profile .profileDetails").on("click", ".profileRowValue label", function(e) {
        e.preventDefault();
        e.stopPropagation();
        showEditBox($(this));
    });
}

export function onValueChanged(callback) {
    
}

function showEditBox(fieldToEdit) {
    const value = fieldToEdit.find("span.value").text();
    const name = fieldToEdit.attr("name");

    let editBox;
    editBox = defaultEditBox(name, value);

    fieldToEdit.after(editBox);
    fieldToEdit.hide();
}

function defaultEditBox(name, value) {
    return `
        <div>
            <input name="${name}" value="${value}" type="text"/>
        </div>
        `;
}

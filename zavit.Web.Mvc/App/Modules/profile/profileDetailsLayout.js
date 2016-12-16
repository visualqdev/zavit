import * as ProfileGender from "./profileGender";

export function attachEvents(options) {
    $("#profile .profileDetails").on("click", ".profileRowValue label", function(e) {
        e.preventDefault();
        e.stopPropagation();
        showEditBox($(this));
    });

    $("#profile .profileDetails").on("click", ".editProfileCancel", function(e) {
        e.stopPropagation();
        const container = $(this).closest(".editProfileControls");
        const labelName = container.find(".editProfileValue").attr("name");
        $(`#profile .profileRowValueLabel[name='${labelName}']`).show();
        container.remove();
    });

    $("#profile .profileDetails").on("click", ".editProfileSave", function(e) {
        e.stopPropagation();
        const container = $(this).closest(".editProfileControls");
        const valueHolder = container.find(".editProfileValue");
        const name = valueHolder.attr("name");
        let value;

        switch (name) {
            case "Gender":
                value = valueHolder.find("option:selected").text();
                break;
            default:
                value = valueHolder.val();
        }

        options.onValueChanged(name, value);
    });
}

export function finishEditing(name, value) {
    const editContainer = $(`#profile .editProfileControls [name='${name}']`).closest(".editProfileControls");
    const label = $(`#profile .profileRowValueLabel[name='${name}']`);

    if (editContainer.length) {
        label.find(".value").text(value);
        editContainer.remove();
        label.show();
    }
}

function showEditBox(fieldToEdit) {
    const value = fieldToEdit.find("span.value").text();
    const name = fieldToEdit.attr("name");

    let editBox;

    switch (name) {
        case "Gender":
            editBox = genderDropDown(value);
            break;
        case "About":
            editBox = aboutEditBox(value);
            break;
        default:
            editBox = defaultEditBox(name, value);
    }

    fieldToEdit.after(editBox);
    fieldToEdit.hide();
}

function defaultEditBox(name, value) {
    return `
        <div class="editProfileControls">
            <input class="editProfileValue editProfileText" name="${name}" value="${value}" type="text"/>
            <input class="editProfileSave btn btn-primary" value="Save" type="button"/>
            <span class="editProfileCancel"><i class="fa fa-times" aria-hidden="true"></i></span>       
        </div>
        `;
}

function genderDropDown(value) {
    return `
        <div class="editProfileControls">
            <select class="editProfileValue editProfileSelect" name="Gender">
                ${genderOptions(value)}
            </select>
            <input class="editProfileSave btn btn-primary" value="Save" type="button"/>
            <span class="editProfileCancel"><i class="fa fa-times" aria-hidden="true"></i></span>       
        </div>
        `;
}

function genderOptions(value) {
    let options = "";

    ProfileGender.allGenderOptions.forEach((genderOption, index) => options += `
        <option ${(ProfileGender.index(value) === index ? "selected=selected" : "")}>${ProfileGender.displayName(index)}</option>
        `);

    return options;
}

function aboutEditBox(value) {
    return `
        <div class="editProfileControls">
            <textarea class="editProfileValue editProfileLongText" name="About" rows="4">${value}</textarea>
            <div class="pull-right">
                <input class="editProfileSave btn btn-primary" value="Save" type="button"/>
                <span class="editProfileCancel"><i class="fa fa-times" aria-hidden="true"></i></span>       
            </div>
        </div>
        `;
}
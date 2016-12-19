import * as ProfileGender from "../../modules/profile/profileGender";
import { html } from "../../modules/htmlUtils/htmlUtil";

export function getView(profile) {
    const view = `
        <div id="profile" class="container">
            <div id="profileHeading">
                <h2 id="mainHeading">My profile</h2>
            </div>
            <div class="profileDetailsContainer">
                <div class="profileImageContainer">
                    <div class="profileImage"><div><i class="fa fa-user" aria-hidden="true"></i></div></div>
                </div>
                <div class="profileDetails">
                    ${profileDetailsRow("Display name", profile.DisplayName, "DisplayName")}
                    ${profileDetailsRow("Email", profile.Email, "Email")}
                    ${profileDetailsRow("Gender", ProfileGender.displayName(profile.Gender), "Gender")}
                    ${profileDetailsRow("About", profile.About, "About")}
                </div>
            </div>            
        </div>
        `;

    return view;
}

function profileDetailsRow(label, value, name) {
    return html`
        <div class="row">
            <div class="col-sm-3 col-xs-12 profileRowLabel">
                <label>${label}</label>
            </div>
            <div class="col-sm-9 col-xs-12 profileRowValue">                            
                <label name="${name}" class="profileRowValueLabel"><span class="value">${value}</span>  <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></label>                
            </div>
        </div>
        `;
}
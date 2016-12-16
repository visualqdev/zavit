import * as AuthorizedClient from "../clients/authorizedClient";
import * as ApiSettings from "../settings/apiSettings";

export function getProfile() {
    const myProfileUrl = `${ApiSettings.apiUrl}api/profiles/myprofile`;

    return AuthorizedClient
        .send({
            url: myProfileUrl,
            type: "get",
            contentType: "application/json; charset=utf-8"
        });
}

export function saveProfile(profile) {
    const postProfileUrl = `${ApiSettings.apiUrl}api/profiles`;

    return AuthorizedClient
        .send({
            url: postProfileUrl,
            type: "post",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(profile)
        });
}
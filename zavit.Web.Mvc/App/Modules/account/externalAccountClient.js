import * as ApiSettings from "../settings/apiSettings";

const obtainLocalAccessTokenUrl = ApiSettings.apiUrl + "api/externalaccounts/obtainlocalaccesstoken";
const registerExternalUrl = ApiSettings.apiUrl + "api/externalaccounts/registerexternal";

export function registerExternal(externalAccessToken, provider, displayName, email) {
    var data = {
        externalAccessToken, 
        provider, 
        displayName, 
        email
    };

    return new Promise((resolve, reject) => 
        $.ajax({
            url: registerExternalUrl,
            type: "post",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            success: resolve,
            error: reject
        })
    );
}

export function obtainLocalAccessToken(externalAccessToken, provider) {
    return new Promise((resolve, reject) => 
        $.ajax({
            url: `${obtainLocalAccessTokenUrl}?provider=${provider}&externalaccesstoken=${externalAccessToken}`,
            type: "get",
            contentType: "application/json; charset=utf-8",
            success: resolve,
            error: reject
        })
    );
}
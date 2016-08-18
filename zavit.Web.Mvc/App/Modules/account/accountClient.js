import * as ApiSettings from "../settings/apiSettings";

const tokenUrl = ApiSettings.apiUrl + "token";

export function getAuthenticationTokens(email, password) {
    var data = {
        username: email,
        password,
        grant_type: "password",
        client_id: 1
    };

    return new Promise((resolve, reject) => 
        $.ajax({
            url: tokenUrl,
            type: "post",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            data: data,
            success: resolve,
            error: reject
        })
    );
}
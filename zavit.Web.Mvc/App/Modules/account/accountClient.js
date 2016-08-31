import * as ApiSettings from "../settings/apiSettings";

const tokenUrl = ApiSettings.apiUrl + "token";
const accountsUrl = ApiSettings.apiUrl + "api/accounts";

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

export function register(displayName, email, password) {
    var data = {
        username: email,
        password,
        displayName
    };

    return new Promise((resolve, reject) => 
        $.ajax({
            url: accountsUrl,
            type: "post",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: resolve,
            error: (err) => {
                if (err.status && err.status === 200) {
                    resolve();
                } else {
                    let errMessage = "Account could not be registered";
                    try {
                        if (err.message) {
                            errMessage = err.message;
                        } else if (err.responseText) {
                            const messageObject = JSON.parse(err.responseText);
                            errMessage = messageObject.Message;
                        }
                    } catch (ex) {
                    }

                    reject(errMessage);
                }
            }
        })
    );
}
import * as ApiSettings from "../settings/apiSettings";

export function getAllActivities() {
    const activitiesUrl = `${ApiSettings.apiUrl}api/activities`;

    return new Promise((resolve, reject) => 
        $.ajax({
            url: activitiesUrl,
            type: "get",
            contentType: "application/json; charset=utf-8",
            success: resolve,
            error: reject
        })
    );
}
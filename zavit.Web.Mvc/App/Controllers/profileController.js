import * as IndexView from "../views/profile/indexView";
import * as Routes from "../routing/routes";
import * as MainContent from "../layout/mainContent";
import * as ProfileClient from "../modules/profile/profileClient";
import * as ProfileDetailsLayout from "../modules/profile/profileDetailsLayout";
import * as PostLoginRedirect from "../modules/account/postLoginRedirect";
import * as Progress from "../modules/loading/progress";

let profile;

export function index() {
    MainContent.load(Routes.profile);

    ProfileClient
        .getProfile()
        .then(profileResult => {
            profile = profileResult;
            const view = IndexView.getView(profile);
            MainContent.append(view);
            attachEditEvents();
        })
        .catch((error) => {
            if (error && error.status && error.status === 401) {
                PostLoginRedirect.storeRedirectUrl(window.location.href);
                Routes.goTo(Routes.login);
            }
        });
}

function attachEditEvents() {
    ProfileDetailsLayout.attachEvents({
        onValueChanged: (name, value) => {
            profile[name] = value;

            ProfileClient.saveProfile(profile)
                .then(() => {
                    ProfileDetailsLayout.finishEditing(name, value);
                });
        },
        onProfileImageSelected: changeProfileImage
    });
}

function changeProfileImage(imageData) {
    Progress.start();
    ProfileClient
        .updateProfileImage(imageData)
        .then(uploadedImage => {
            Progress.done();
            ProfileDetailsLayout.updateProfileImage(uploadedImage.ProfileImageUrl);
        })
        .catch(Progress.done);
}
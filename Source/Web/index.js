class index extends doLittle.views.ViewModel {
    constructor(userManager) {
        userManager.getUser().then(user => {
            if( !user ) return;
            var url = "http://localhost:5000/Authentication/Identity";

            var xhr = new XMLHttpRequest();
            xhr.open("GET", url);
            xhr.onload = function () {
                console.log(xhr.status, xhr.responseText);
            }
            xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
            xhr.send();

            if( user ) {

            }
        });
    }
}

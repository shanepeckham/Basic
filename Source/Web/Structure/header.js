class header extends doLittle.views.ViewModel
{
    constructor(authenticationSchemes, userManager) {
        this.authenticationSchemes = authenticationSchemes.all();
        this.userManager = userManager;
    }

    signIn() {
        this.userManager.signinRedirect();

        /*
        this.userManager.getUser().then((user) => {
            console.log("Hello");
            debugger;

        });*/

    }
}
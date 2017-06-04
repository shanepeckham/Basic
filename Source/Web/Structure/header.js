class header extends doLittle.views.ViewModel
{
    constructor(authenticationSchemes) {
        this.authenticationSchemes = authenticationSchemes.all();
    }

    signIn() {
        console.log("Signing in");

    }
}
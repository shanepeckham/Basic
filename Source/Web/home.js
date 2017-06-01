const _someProperty = new WeakMap();

class home extends doLittle.views.ViewModel {
    constructor(something) {
        console.log("HOME");

        this.stuff = "asd";

        this.doStuff();

        this.someProperty = "Awesome stuff";
        console.log("The property is saying: "+this.someProperty);
    }

    get someProperty() {
        console.log("I'm being called");
        return _someProperty.get(this);
    }
    set someProperty(value) {
        _someProperty.set(this, value);
    }

    doStuff(arg1, arg2) {
        console.log("Doing Shit");

    }
}


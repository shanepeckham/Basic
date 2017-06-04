class Registration extends doLittle.views.ViewModel
{
    constructor(register, employees) {
        this.register = register;
        this.employees = employees.all();
    }
}

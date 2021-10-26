import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { Validators } from '@angular/forms';
let RegisterComponent = class RegisterComponent {
    constructor(fb, userService, toastr, router, loader) {
        this.fb = fb;
        this.userService = userService;
        this.toastr = toastr;
        this.router = router;
        this.loader = loader;
        this.emailRegex = '[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,63}$';
        this.phoneRegex = '[- +()0-9]+';
        this.form = fb.group({
            email: ['', [Validators.required, Validators.pattern(this.emailRegex), Validators.maxLength(30)], []],
            passwords: this.fb.group({
                password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(100)], []],
                confirmPassword: ['', [Validators.required], []]
            }, { validator: this.confirmPasswords }),
            firstName: ['', [Validators.required, Validators.maxLength(100)], []],
            lastName: ['', [Validators.required, Validators.maxLength(100)], []],
            phone: ['', [Validators.required, Validators.maxLength(20), Validators.pattern(this.phoneRegex)], []]
        });
    }
    ngOnInit() {
        this.form.reset();
    }
    ;
    onSubmit(formData) {
        this.userService.register(formData).subscribe((response) => {
            if (response.succeeded) {
                this.form.reset();
                this.toastr.success("Registration successful.", 'Please log in.');
                this.router.navigate(['/user/login']);
            }
            else {
                response.errors.forEach(element => {
                    switch (element.code) {
                        case 'DuplicateUserName':
                            this.toastr.error('Username already taken!', 'Registration failed.');
                            break;
                        default:
                            this.toastr.error(element.description, 'Registration failed.');
                            break;
                    }
                });
            }
        }, err => {
            console.log(err);
        });
    }
    //Validate the two password in the form input fields
    confirmPasswords(group) {
        let confirmPassword = group.get('confirmPassword');
        if (confirmPassword.errors == null || 'passwordMismatch' in confirmPassword.errors) {
            if (group.get('password').value != confirmPassword.value) {
                confirmPassword.setErrors({ passwordMismatch: true });
            }
            else {
                confirmPassword.setErrors(null);
            }
        }
    }
};
RegisterComponent = __decorate([
    Component({
        selector: 'app-register',
        templateUrl: './register.component.html',
        styleUrls: ['./register.component.css']
    })
], RegisterComponent);
export { RegisterComponent };
//# sourceMappingURL=register.component.js.map
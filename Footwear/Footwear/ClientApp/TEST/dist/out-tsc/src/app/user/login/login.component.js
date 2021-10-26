import { __decorate } from "tslib";
import { Component } from '@angular/core';
let LoginComponent = class LoginComponent {
    constructor(userService, router, toastr, cookieService, loader) {
        this.userService = userService;
        this.router = router;
        this.toastr = toastr;
        this.cookieService = cookieService;
        this.loader = loader;
    }
    //The method will prevent any user from accessing the login view, who is already authenticated
    ngOnInit() {
        if (this.cookieService.get('token') != '') {
            this.router.navigate(['/']);
        }
    }
    //Send the token to the Web API to authenticate
    onSubmit(form) {
        this.userService.login(form.value).subscribe((response) => {
            this.cookieService.set('token', response.token);
            this.router.navigateByUrl('/');
        }, error => {
            if (error.status == 400) { //bad request from the api
                this.toastr.error('Incorrect username and password.', 'Authentication failed.');
            }
            else {
                console.log(error);
            }
        });
    }
};
LoginComponent = __decorate([
    Component({
        selector: 'app-login',
        templateUrl: './login.component.html',
        styleUrls: ['./login.component.css']
    })
], LoginComponent);
export { LoginComponent };
//# sourceMappingURL=login.component.js.map
import { __decorate, __param } from "tslib";
import { Injectable, Inject } from '@angular/core';
let UserService = class UserService {
    /*userName: string;*/
    constructor(fb, http, baseUrl) {
        this.fb = fb;
        this.http = http;
        this.baseUrl = baseUrl;
    }
    register(formData) {
        var body = {
            Email: formData.email,
            Password: formData.passwords.password,
            FirstName: formData.firstName,
            LastName: formData.lastName,
            Phone: formData.phone
        };
        return this.http.post(this.baseUrl + 'user/register', body);
    }
    login(formData) {
        return this.http.post(this.baseUrl + 'user/login', formData);
    }
    getUserProfile() {
        return this.http.get(this.baseUrl + 'user/getProfileData');
    }
    updateUserProfile(formData) {
        var body = {
            Email: formData.email,
            Password: formData.passwords.password,
            FirstName: formData.firstName,
            LastName: formData.lastName,
            Phone: formData.phone,
            Address: {
                "street": formData.street,
                "city": formData.city,
                "state": formData.state,
                "country": formData.country,
                "zip": formData.zip
            }
        };
        return this.http.put(this.baseUrl + 'user/userProfile', body);
    }
};
UserService = __decorate([
    Injectable({
        providedIn: 'root'
    }),
    __param(2, Inject('BASE_URL'))
], UserService);
export { UserService };
//# sourceMappingURL=user.service.js.map
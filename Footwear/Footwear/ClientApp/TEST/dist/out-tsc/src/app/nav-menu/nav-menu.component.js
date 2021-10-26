import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { faShoppingCart, faSignOutAlt, faSignInAlt, faAddressCard, faDatabase, faUser } from '@fortawesome/free-solid-svg-icons';
let NavMenuComponent = class NavMenuComponent {
    constructor(router, userService, cookieService) {
        this.router = router;
        this.userService = userService;
        this.cookieService = cookieService;
        this.isExpanded = false;
        //FontAwesomeIcons:
        this.faShoppingCart = faShoppingCart;
        this.faSignOutAlt = faSignOutAlt;
        this.faSignInAlt = faSignInAlt;
        this.faAddressCard = faAddressCard;
        this.faDatabase = faDatabase;
        this.faUser = faUser;
    }
    ngOnInit() {
        this.userService.getUserProfile().subscribe(response => {
            this.userDetails = response;
        }, error => {
            console.log(error);
        });
    }
    collapse() {
        this.isExpanded = false;
    }
    toggle() {
        this.isExpanded = !this.isExpanded;
    }
    onLogout() {
        this.cookieService.delete('token');
        this.router.navigate(['/user/login']);
    }
    isAuthenticated() {
        if (this.cookieService.get('token') != '') {
            return true;
        }
        else {
            return false;
        }
    }
};
NavMenuComponent = __decorate([
    Component({
        selector: 'app-nav-menu',
        templateUrl: './nav-menu.component.html',
        styleUrls: ['./nav-menu.component.css']
    })
], NavMenuComponent);
export { NavMenuComponent };
//# sourceMappingURL=nav-menu.component.js.map
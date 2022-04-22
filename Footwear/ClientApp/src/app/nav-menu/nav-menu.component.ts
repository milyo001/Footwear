import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';
import {
  faShoppingCart, faSignOutAlt,
  faSignInAlt, faAddressCard,
  faBoxOpen,faUser, faCubes,
  faInfoCircle }
from '@fortawesome/free-solid-svg-icons';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  isExpanded = false;

  //FontAwesomeIcons:
  faShoppingCart = faShoppingCart;
  faSignOutAlt = faSignOutAlt;
  faSignInAlt = faSignInAlt;
  faAddressCard = faAddressCard;
  faBoxOpen = faBoxOpen;
  faUser = faUser;
  faCubes = faCubes;
  faInfoCircle = faInfoCircle;

  constructor(
    private router: Router,
    public userService: UserService,
    private cookieService: CookieService) { }
    
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

  isAuthenticated(): boolean {
    if (this.cookieService.get('token') != '') {
      return true;
    }
    else { return false }
  }
}

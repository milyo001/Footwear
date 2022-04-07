import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';
import {
  faShoppingCart, faSignOutAlt, faSignInAlt, faAddressCard,
  faDatabase, 
  faUser} from '@fortawesome/free-solid-svg-icons';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  isExpanded = false;
  userDetails;

  //FontAwesomeIcons:
  faShoppingCart = faShoppingCart;
  faSignOutAlt = faSignOutAlt;
  faSignInAlt = faSignInAlt;
  faAddressCard = faAddressCard;
  faDatabase = faDatabase;
  faUser = faUser;

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

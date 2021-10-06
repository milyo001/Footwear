import { Component, OnInit } from '@angular/core';
import { CartService } from '../services/cart.service';
import { IProduct } from '../interfaces';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';
import {
  faShoppingCart, faSignOutAlt, faSignInAlt, faAddressCard,
  faDatabase } from '@fortawesome/free-solid-svg-icons';
import { Local } from 'protractor/built/driverProviders';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit{

  isExpanded = false;
  userDetails;

  //FontAwesomeIcons:
  faShoppingCart = faShoppingCart;
  faSignOutAlt = faSignOutAlt;
  faSignInAlt = faSignInAlt;
  faAddressCard = faAddressCard;
  faDatabase = faDatabase;

  constructor(
    private router: Router,
    public userService: UserService,
    private cookieService: CookieService) { }
    

  ngOnInit(): void {
    this.userService.getUserProfile().subscribe(
      response => {
        this.userDetails = response;
      },
      error => {
        console.log(error)
      }
    )
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

  isAuthenticated(): boolean {
    if (this.cookieService.get('token') != '') {
      return true;
    }
    else { return false }
  }


}

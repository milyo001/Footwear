import { Component, OnInit } from '@angular/core';
import { CartService } from '../services/cart.service';
import { IProduct } from '../interfaces';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit{

  isExpanded = false;
  userDetails;

  cartItems: IProduct[] = this.cartService.getItems();

  constructor(
    private cartService: CartService,
    private router: Router,
    public userService: UserService) { }

  ngOnInit(): void {
    this.userService.userName = this.userService.initUserName();

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
    localStorage.removeItem('token');
    localStorage.setItem('userName', '');
    this.userService.userName = '';
    this.router.navigate(['/user/login']);
  }

  isAuthenticated() {
    if (localStorage.getItem('token')) {
      return true;
    }
    return false;
  }

}

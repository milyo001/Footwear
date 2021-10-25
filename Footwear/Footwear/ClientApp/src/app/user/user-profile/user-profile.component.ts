import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IUserData } from '../../interfaces/userData';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  public userData: IUserData = null;
  formGroup: FormGroup;


  constructor(private userService: UserService, private fb: FormBuilder,) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.userService.getUserProfile().subscribe(data => {
      this.userData = data as IUserData;
    })
  }
  updateProfile() {

  }
}

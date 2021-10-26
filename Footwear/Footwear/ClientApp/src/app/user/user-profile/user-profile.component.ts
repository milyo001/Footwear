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
  form: FormGroup;
  private phoneRegex: string = '[- +()0-9]+';



  constructor(private userService: UserService, private fb: FormBuilder,) {

  }

  ngOnInit(): void {
    this.loadData();

  }

  loadData() {
    this.userService.getUserProfile().subscribe(data => {
      this.userData = data as IUserData;
      this.form = this.fb.group({
        firstName: [data.firstName, [Validators.required, Validators.maxLength(100)], []],
        lastName: [data.lastName, [Validators.required, Validators.maxLength(100)], []],
        phone: [data.phone, [Validators.required, Validators.maxLength(20), Validators.pattern(this.phoneRegex)], []],
        street: [data.street, [Validators.required, Validators.maxLength(100)],[]],
        state: [data.state, [Validators.required, Validators.maxLength(20)], []],
        country: [data.country, [Validators.required, Validators.maxLength(20)], []],
        city: [data.city, [Validators.required, Validators.maxLength(20)], []],
        zipCode: [data.zipCode, [Validators.required, Validators.maxLength(20)], []]

      })
    })

  }
  updateProfile(form) {

  }
}

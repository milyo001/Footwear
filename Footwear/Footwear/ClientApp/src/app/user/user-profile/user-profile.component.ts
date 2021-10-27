import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
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

  constructor(private userService: UserService, private fb: FormBuilder,private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadData();

  }

  loadData() {
    this.userService.getUserProfile().subscribe(data => {
      this.userData = data as IUserData;
      this.form = this.fb.group({
        email: [data.email],
        firstName: [data.firstName, [Validators.required, Validators.maxLength(100)], []],
        lastName: [data.lastName, [Validators.required, Validators.maxLength(100)], []],
        phone: [data.phone, [Validators.required, Validators.maxLength(20), Validators.pattern(this.phoneRegex)], []],
        street: [data.street, [Validators.required, Validators.maxLength(100), Validators.minLength(2)], []],
        state: [data.state, [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
        country: [data.country, [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
        city: [data.city, [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []],
        zipCode: [data.zipCode, [Validators.required, Validators.maxLength(20), Validators.minLength(2)], []]

      })
    })

  }
  updateProfile(form: any) {
    this.userService.updateUserProfile(form.value).subscribe(
      (response: any) => {
        if (response.succeeded) {
          this.toastr.success("Successfully updated user information!");
          this.loadData();
        }
        else {
          response.errors.forEach(element => { 
           this.toastr.error(element.description, 'Update failed!');
          })
        }
      },
      err => {
        console.log(err);
      }
    );
  }
}

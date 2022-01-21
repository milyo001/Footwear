import { Injectable, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IUserData } from '../interfaces/userData';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseUrl: string;

  constructor(private fb: FormBuilder, private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
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

  login(formData: any) {
    return this.http.post(this.baseUrl + 'user/login', formData);
  }

  getUserProfile(): Promise<IUserData> {
    return this.http.get<IUserData>(this.baseUrl + 'user/getProfileData').toPromise();
  }

  updateUserProfile(formData: any) {
    return this.http.put(this.baseUrl + 'user/updateUserProfile', formData);
  }

  updateEmail(formData: any) {
    return this.http.put(this.baseUrl + 'user/updateEmail', formData);
  }

  updatePassword(formData: any) {
    return this.http.put(this.baseUrl + 'user/updatePassword', formData);
  }
}

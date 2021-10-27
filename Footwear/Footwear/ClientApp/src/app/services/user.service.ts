import { Injectable, Inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IUserData } from '../interfaces/userData';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseUrl: string;

  /*userName: string;*/

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

  login(formData) {
    return this.http.post(this.baseUrl + 'user/login', formData);
  }

  getUserProfile(): Observable<IUserData> {
    return this.http.get<IUserData>(this.baseUrl + 'user/getProfileData');
  }

  updateUserProfile(formData: any) {
    return this.http.put(this.baseUrl + 'user/updateUserProfile', formData);
  }

  
}

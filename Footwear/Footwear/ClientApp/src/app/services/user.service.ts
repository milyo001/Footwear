import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IUserData } from '../interfaces/user/userData';
import { IRegisterData } from '../interfaces/user/registerData';
import { ILoginData } from '../interfaces/user/loginData';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  register(registerData: IRegisterData) {
    return this.http.post(this.baseUrl + 'user/register', registerData);
  }

  login(loginData: ILoginData) {
    return this.http.post(this.baseUrl + 'user/login', loginData);
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

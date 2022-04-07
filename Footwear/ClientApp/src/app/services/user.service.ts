import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IUserData } from '../interfaces/user/userData';
import { IRegisterData } from '../interfaces/user/registerData';
import { ILoginData } from '../interfaces/user/loginData';
import { IEmailData } from '../interfaces/user/emailData';
import { IPasswordData } from '../interfaces/user/passwordData';
import { getAPIUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl: string = getAPIUrl();

  constructor(private http: HttpClient) { }

  register(registerData: IRegisterData) {
    return this.http.post(this.apiUrl + 'user/register', registerData);
  }

  login(loginData: ILoginData) {
    return this.http.post(this.apiUrl + 'user/login', loginData);
  }

  getUserProfile(): Promise<IUserData> {
    return this.http.get<IUserData>(this.apiUrl + 'user/getProfileData').toPromise();
  }

  updateUserProfile(userData: IUserData) {
    return this.http.put(this.apiUrl + 'user/updateUserProfile', userData);
  }

  updateEmail(emailData: IEmailData) {
    return this.http.put(this.apiUrl + 'user/updateEmail', emailData);
  }

  updatePassword(passwordData: IPasswordData) {
    return this.http.put(this.apiUrl + 'user/updatePassword', passwordData);
  }
}

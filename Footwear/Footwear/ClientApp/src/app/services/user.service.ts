import { Injectable, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseUrl: string;

  userName: string;

  constructor(private fb: FormBuilder, private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  register(formData) {
    var body = {
      Email: formData.email,
      Password: formData.passwords.password,
      FirstName: formData.firstName,
      LastName: formData.lastName,
      Phone: formData.phone,
      Address: formData.address
    };
    return this.http.post(this.baseUrl + 'user/register', body);
  }

  login(formData) {
    localStorage.setItem('userName', formData.email);
    this.userName = formData.email;
    return this.http.post(this.baseUrl + 'user/login', formData);

  }

  getUserProfile() {
    return this.http.get(this.baseUrl + 'userprofile');
  }

  
  initUserName(): string {
    return this.userName = localStorage.getItem('userName');
  }
  
}

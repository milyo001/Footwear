import { Injectable, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

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
      Phone: formData.phone,
      Address: formData.address
    };
    console.log(body);
    return this.http.post(this.baseUrl + 'user/register', body);
  }
  
}

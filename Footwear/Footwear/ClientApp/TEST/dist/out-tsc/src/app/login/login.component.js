//import { Component } from '@angular/core';
//import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
//@Component({
//  selector: 'app-login',
//  templateUrl: './login.component.html',
//  styleUrls: ['./login.component.css']
//})
//export class LoginComponent {
//  formGroup: FormGroup;
//  constructor(private formBuilder: FormBuilder) { }
//  ngOnInit() {
//    this.createForm();
//  }
//  createForm() {
//    this.formGroup = this.formBuilder.group({
//      'username': ['', Validators.required],
//      'password': ['', Validators.required],
//    });
//  }
//  getError(el) {
//    switch (el) {
//      case 'user':
//        if (this.formGroup.get('username').hasError('required')) {
//          return 'Username required';
//        }
//        break;
//      case 'pass':
//        if (this.formGroup.get('password').hasError('required')) {
//          return 'Password required';
//        }
//        break;
//      default:
//        return '';
//    }
//  }
//  onSubmit(post) {
//    // this.post = post;
//  }
//}
//# sourceMappingURL=login.component.js.map
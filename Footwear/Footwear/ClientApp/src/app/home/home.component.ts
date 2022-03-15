import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

import { LoadingService } from '../services/loading.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  loading = this.loader.loading;

  constructor(public loader: LoadingService, private userService: UserService) { }

  
  
  
}

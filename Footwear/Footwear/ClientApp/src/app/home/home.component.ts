import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { IProduct } from '../interfaces';
import { LoadingService } from '../services/loading.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  loading = this.loader.loading;

  constructor(public loader: LoadingService, private http: HttpClient) {

  }

  
  
  
}

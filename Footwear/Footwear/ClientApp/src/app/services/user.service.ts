import { Injectable, Inject } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IUser } from '../interfaces/user';
import { catchError, tap } from 'rxjs/operators';

@Injectable()
export class UserService {

  currentUser: IUser | null;

  baseUrl: string;

  get isLogged(): boolean { return !!this.currentUser; }

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getCurrentUserProfile(): Observable<any> {
    return this.http.get(`${this.baseUrl}/users/profile`, { withCredentials: true }).pipe(
      tap(((user: IUser) => this.currentUser = user)),
      catchError(() => { this.currentUser = null; return of(null); })
    );
  }

  login(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/users/login`, data, { withCredentials: true }).pipe(
      tap((user: IUser) => this.currentUser = user)
    );
  }

  register(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/users/register`, data, { withCredentials: true }).pipe(
      tap((user: IUser) => this.currentUser = user)
    );
  }

  logout(): Observable<any> {
    return this.http.post(`${this.baseUrl}/users/logout`, {}, { withCredentials: true }).pipe(
      tap(() => this.currentUser = null)
    );
  }

  updateProfile(data: any): Observable<IUser> {
    return this.http.put(`${this.baseUrl}/users/profile`, data, { withCredentials: true }).pipe(
      tap((user: IUser) => this.currentUser = user)
    );
  }
}

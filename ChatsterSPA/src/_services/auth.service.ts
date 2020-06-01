import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { User } from 'src/_models/user';

@Injectable()
export class AuthService {
  constructor(private http: HttpClient) {}
  jwtHelper = new JwtHelperService();
  decodedToken: any = {};
  baseUrl = environment.apiBaseUrl + 'auth/';
  photoUrl = new BehaviorSubject<string>('../../assets/user.png');
  currentPhotoUrl = this.photoUrl.asObservable();

  loggedInUser: User;

  login(loginData: any) {
    return this.http.post(this.baseUrl + 'login', loginData).pipe(
      map((response: any) => {
        if (response) {
          localStorage.setItem('token', response.token);
          localStorage.setItem(
            'logged_in_user',
            JSON.stringify(response.loggedInUser)
          );
          this.loggedInUser = response.loggedInUser;
          this.decodedToken = this.jwtHelper.decodeToken(response.token);
          this.changeMemberPhoto(this.loggedInUser.photoUrl);
        }
      })
    );
  }

  changeMemberPhoto(photoUrl: string) {
    this.photoUrl.next(photoUrl);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  register(user: User) {
    return this.http.post(this.baseUrl + 'register', user);
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  reload() {
    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('logged_in_user'));

    if (token) {
      this.decodedToken = this.jwtHelper.decodeToken(token);
    }

    if (user) {
      this.loggedInUser = user;
      this.changeMemberPhoto(user.photoUrl);
    }
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('logged_in_user');
    this.decodedToken = null;
    this.loggedInUser = null;
  }

  roleMatch(allowedRoles) {
    let isMatch = false;

    const userRoles = this.decodedToken.role as Array<string>;

    allowedRoles.forEach((element) => {
      if (userRoles.includes(element)) {
        isMatch = true;
        return isMatch;
      }
    });

    return isMatch;
  }
}

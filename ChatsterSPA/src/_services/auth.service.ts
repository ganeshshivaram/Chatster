import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';

@Injectable()
export class AuthService {
  constructor(private http: HttpClient) {}
  jwtHelper = new JwtHelperService();
  decodedToken: any = {};
  baseUrl = environment.apiBaseUrl + 'auth/';

  login(loginData: any) {
    return this.http.post(this.baseUrl + 'login', loginData).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
        }
      })
    );
  }

  register(registerData: any) {
    return this.http.post(this.baseUrl + 'register', registerData);
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  reload() {
    const token = localStorage.getItem('token');
    if (token) {
      this.decodedToken = this.jwtHelper.decodeToken(token);
    }
  }
}

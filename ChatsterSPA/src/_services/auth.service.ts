import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable()
export class AuthService {
  constructor(private http: HttpClient) {}

  baseUrl = 'http://localhost:5000/api/auth/';

  login(loginData: any) {
    return this.http.post(this.baseUrl + 'login', loginData).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
        }
      })
    );
  }

  register(registerData: any) {
    return this.http.post(this.baseUrl + 'register', registerData);
  }
}

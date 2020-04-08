import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  loginData: any = {};
  constructor(private authService: AuthService) {}

  ngOnInit() {}

  login() {
    this.authService.login(this.loginData).subscribe(
      response => {
        console.log('Logged in successfully');
      },
      error => {
        console.log('Unable to login');
      }
    );
  }

  loggedIn() {
    return !!localStorage.getItem('token');
  }

  logout() {
    localStorage.removeItem('token');
    console.log('Logged out successfully');
  }
}

import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/_services/auth.service';
import { AlertifyService } from 'src/_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  loginData: any = {};
  constructor(
    public authService: AuthService,
    private alertify: AlertifyService,
    private router: Router
  ) {}

  ngOnInit() {}

  login() {
    this.authService.login(this.loginData).subscribe(
      (response) => {
        this.alertify.success('Logged in successfully');
      },
      (error) => {
        this.alertify.error('Unable to login');
      },
      () => {
        this.router.navigate(['/members']);
      }
    );
  }

  loggedIn() {
    return this.authService.isLoggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('Logged out successfully');
    this.router.navigate(['/home']);
  }
}

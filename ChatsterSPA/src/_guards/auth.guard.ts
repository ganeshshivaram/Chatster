import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from 'src/_services/auth.service';
import { AlertifyService } from 'src/_services/alertify.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private authService: AuthService,
    private alertify: AlertifyService
  ) {}

  canActivate(next: ActivatedRouteSnapshot): boolean {
    var roles = next.firstChild.data.roles;

    if (roles && this.authService.isLoggedIn()) {
      const match = this.authService.roleMatch(roles);

      if (match) {
        return true;
      } else {
        this.router.navigate(['/members']);
        this.alertify.error('You are not authorized to view this page');
      }
    }
    if (this.authService.isLoggedIn()) {
      return true;
    }

    this.alertify.error('Unauthorized access detected');
    this.router.navigate(['home']);
  }
}

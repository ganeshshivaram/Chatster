import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { UserService } from 'src/_services/user.service';
import { AlertifyService } from 'src/_services/alertify.service';
import { catchError } from 'rxjs/operators';
import { User } from 'src/_models/user';

@Injectable({ providedIn: 'root' })
export class ListResolver implements Resolve<User[]> {
  constructor(
    private userService: UserService,
    private alertify: AlertifyService,
    private router: Router
  ) {}
  resolve(route: ActivatedRouteSnapshot): Observable<User[]> {
    return this.userService.getUsers(1, 5, null, 'likers').pipe(
      catchError(() => {
        this.alertify.error('Problem retreiving the data');
        this.router.navigate(['/home']);
        return of(null);
      })
    );
  }
}

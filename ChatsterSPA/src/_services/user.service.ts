import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/_models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  public baseUrl = environment.apiBaseUrl + 'users';

  public getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl);
  }

  public getUser(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + '/' + id);
  }

  public updateUser(id: number, user: User) {
    return this.http.put(this.baseUrl + '/' + id, user);
  }

  public setMainPhoto(userId: number, id: number) {
    return this.http.post(
      this.baseUrl + '/' + userId + '/photos/' + id + '/setMainPhoto',
      {}
    );
  }

  public deletePhoto(userId: number, id: number) {
    return this.http.delete(this.baseUrl + '/' + userId + '/photos/' + id);
  }
}

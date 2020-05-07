import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/_models/user';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from 'src/_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  public baseUrl = environment.apiBaseUrl + 'users';

  public getUsers(
    pageNumber?,
    pageSize?,
    userParams?
  ): Observable<PaginatedResult<User[]>> {
    const paginatedResult = new PaginatedResult<User[]>();
    let params = new HttpParams();

    if (pageNumber != null && pageSize != null) {
      params = params.append('pageNumber', pageNumber);
      params = params.append('pageSize', pageSize);
    }

    if (userParams != null) {
      params = params.append('minAge', userParams.minAge);
      params = params.append('maxAge', userParams.maxAge);
      params = params.append('gender', userParams.gender);
      params = params.append('orderBy', userParams.orderBy);
    }

    return this.http
      .get<User[]>(this.baseUrl, {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }
          return paginatedResult;
        })
      );
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

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/_models/user';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from 'src/_models/pagination';
import { map } from 'rxjs/operators';
import { Message } from 'src/_models/Message';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  public baseUrl = environment.apiBaseUrl + 'users';

  public getUsers(
    pageNumber?,
    pageSize?,
    userParams?,
    likeParams?
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

    if (likeParams === 'likers') {
      params = params.append('likers', 'true');
    }

    if (likeParams === 'likees') {
      params = params.append('likees', 'true');
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

  public sendLike(userId: number, recipientId: number) {
    return this.http.post(
      this.baseUrl + '/' + userId + '/like/' + recipientId,
      {}
    );
  }

  getMessages(id: number, page?, itemsPerPage?, messageContainer?) {
    const paginatedResult: PaginatedResult<Message[]> = new PaginatedResult<
      Message[]
    >();

    let params = new HttpParams();

    params = params.append('MessageContainer', messageContainer);

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http
      .get<Message[]>(this.baseUrl + '/' + id + '/messages', {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') !== null) {
            paginatedResult.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }

          return paginatedResult;
        })
      );
  }

  getMessageThread(id: number, recipientId: number) {
    return this.http.get<Message[]>(
      this.baseUrl + '/' + id + '/messages/thread/' + recipientId
    );
  }

  sendMessage(id: number, message: Message) {
    return this.http.post(this.baseUrl + '/' + id + '/messages', message);
  }

  deleteMessage(id: number, userId: number) {
    return this.http.delete(
      this.baseUrl + '/' + userId + '/messages/' + id,
      {}
    );
  }

  markAsRead(userId: number, messageId: number) {
    this.http
      .post(
        this.baseUrl + '/' + userId + '/messages/' + messageId + '/read',
        {}
      )
      .subscribe();
  }
}

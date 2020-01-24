import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { UserData } from '../_models/user.data';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl: string = 'http://localhost:57022/api/users';
  // private apiUrl: string = 'http://localhost/ProjectManagementService/api/users';
  constructor(private http: HttpClient) { }

  saveUser(request: any): Observable<UserData> {
    return this.http.post<UserData>(this.apiUrl, request);
  }

  getUsers(): Observable<UserData[]> {
    return this.http.get<UserData[]>(this.apiUrl +'/GetUsers/');
  }
    
  deleteUser(id: number): Observable<UserData> {
    return this.http.get<UserData>(this.apiUrl + '/DeleteUser/' + id);
  }

  getUser(id: number): Observable<UserData> {
    return this.http.get<UserData>(this.apiUrl + '/' + id);
  }
 
  updateUser(id: number, request: any): Observable<UserData> {
    return this.http.put<UserData>(this.apiUrl + '/' + id, request);
  }
}

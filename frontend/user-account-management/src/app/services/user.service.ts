import { Injectable } from '@angular/core';
import { User } from '../interfaces/user';
import { HttpClient } from '@angular/common/http';
import { CreateUserRequest } from '../interfaces/createUserRequest';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  headers = { 'Content-Type': 'application/json; charset=utf-8' };
  users: User[] = [];

  constructor(private http: HttpClient) {
    this.getUsers();
  }

  getUsers() {
    this.http.get<any>(environment.apiUrl).subscribe({
      next: (response) => {
        this.users = response.model;
      },
      error: (e) => {
        console.error('error: ', e);
      }
    });
  }

  public add(request: CreateUserRequest) {
    this.http.post<CreateUserRequest>
      (environment.apiUrl, JSON.stringify(request), { headers: this.headers }).subscribe({
        next: () => {
          this.getUsers();
        },
        error: (e) => {
          console.error('error: ', e);
        }
      });
  }
}

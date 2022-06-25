import { Injectable } from '@angular/core';
import { User } from '../interfaces/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CreateUserRequest } from '../interfaces/createUserRequest';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  apiUrl = 'https://localhost:7018/user';
  headers = { 'Content-Type': 'application/json; charset=utf-8' };
  users: User[] = [];

  constructor(private http: HttpClient) {
  }

  getUsers() {
    this.http.get<any>(this.apiUrl).subscribe({
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
      (this.apiUrl, JSON.stringify(request), { headers: this.headers }).subscribe({
        next: () => {
          this.getUsers();
        },
        error: (e) => {
          console.error('error: ', e);
        }
      });
  }
}
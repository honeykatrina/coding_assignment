import { Injectable } from '@angular/core';
import { User } from '../interfaces/user';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Account } from '../interfaces/account';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  headers = { 'Content-Type': 'application/json; charset=utf-8' };
  users: User[] = [];
  accounts: Account[] = [];

  constructor(private http: HttpClient) {
    this.getUsers();
  }

  getUsers() {
    this.http.get<any>(environment.apiUrl + 'user').subscribe({
      next: (response) => {
        this.users = response.model;
      },
      error: (e) => {
        console.error('error: ', e);
      }
    });
  }

  addAccount(customerId: number, initialCredit: number) {
    this.http.post
      (environment.apiUrl + `user/${customerId}/accounts`, JSON.stringify(initialCredit), { headers: this.headers }).subscribe({
        next: () => {
          this.getAccounts(customerId);
          this.getUsers();
        },
        error: (e) => {
          console.error('error: ', e);
        }
      });
  }

  getAccounts(customerId: number) {
    this.http.get<any>(environment.apiUrl + `user/${customerId}/accounts`).subscribe({
      next: (response) => {
        this.accounts = response.model;
      },
      error: (e) => {
        console.error('error: ', e);
      }
    });
  }
}

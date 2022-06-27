import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Transaction } from '../interfaces/transaction';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  apiUrl = 'https://localhost:7018/user';
  headers = { 'Content-Type': 'application/json; charset=utf-8' };
  transactions: Transaction[] = []

  constructor(private http: HttpClient) {
    this.getTransactions()
  }

  getTransactions() {
    this.http.get<any>(this.apiUrl + "/transactions").subscribe({
      next: (response) => {
        this.transactions = response.model;
      },
      error: (e) => {
        console.error('error: ', e);
      }
    });
  }
}
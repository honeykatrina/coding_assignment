import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Transaction } from '../interfaces/transaction';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  headers = { 'Content-Type': 'application/json; charset=utf-8' };
  transactions: Transaction[] = []

  constructor(private http: HttpClient) {
    this.getTransactions()
  }

  getTransactions() {
    this.http.get<any>(environment.apiUrl + "/transactions").subscribe({
      next: (response) => {
        this.transactions = response.model;
      },
      error: (e) => {
        console.error('error: ', e);
      }
    });
  }
}
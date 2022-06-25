import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Transaction } from '../interfaces/transaction';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  apiUrl = 'https://localhost:7018/user';
  headers = { 'Content-Type': 'application/json; charset=utf-8' };

  constructor(private http: HttpClient) { }

  getTransactionsByUserAccount(accountId: string) {
    return this.http.get<Transaction[]>(this.apiUrl + accountId + "/transactions");
  }
}

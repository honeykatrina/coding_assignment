import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { TransactionService } from 'src/app/services/transaction.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  constructor(
    public userService: UserService,
    public transactionService: TransactionService) {
  }

  ngOnInit(): void {
    this.transactionService.getTransactions();
    this.userService.getUsers();
  }

  getTransactions(accountId: string) {
    let transactions = this.transactionService.transactions.filter(x => x.accountId == accountId);
    return transactions.map(
      x => `${formatDate(x.creationDate, 'dd/MM/yyyy', 'en-US')}: ${x.amount}`
    )
  }
}

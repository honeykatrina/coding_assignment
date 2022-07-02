import { formatDate, Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import { filter } from 'rxjs';
import { TransactionService } from 'src/app/services/transaction.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']
})
export class AccountComponent implements OnInit {

  customerId: number = 0;

  constructor(private router: Router, public transactionService: TransactionService, public userService: UserService) {
    const navigation = router.getCurrentNavigation();
    const state = navigation?.extras.state as { id: number };
    this.customerId = state.id;
    this.userService.getAccounts(this.customerId);
    this.transactionService.getTransactions();
  }

  ngOnInit(): void { }

  getTransactions(accountId: string) {
    let transactions = this.transactionService.transactions.filter(x => x.accountId == accountId);
    return transactions.map(
      x => `${formatDate(x.creationDate, 'dd/MM/yyyy', 'en-US')}: ${x.amount}`
    )
  }
}

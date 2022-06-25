import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/interfaces/user';
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
    this.userService.getUsers()
  }
}

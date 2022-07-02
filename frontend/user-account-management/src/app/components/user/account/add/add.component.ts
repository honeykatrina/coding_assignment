import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {

  addAccount = new FormGroup({
    initialCredit: new FormControl(0, [Validators.required])
  });

  customerId: number = 0;

  constructor(private router: Router, public userService: UserService) {
    const navigation = router.getCurrentNavigation();
    const state = navigation?.extras.state as { id: number };
    this.customerId = state.id;
    this.userService.getAccounts(this.customerId);
  }

  ngOnInit(): void {
  }

  submit() {
    this.userService.addAccount(this.customerId, this.addAccount.value.initialCredit!);
    this.router.navigateByUrl('');
  }
}

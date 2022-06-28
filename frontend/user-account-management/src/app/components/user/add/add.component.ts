import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CreateUserRequest } from 'src/app/interfaces/createUserRequest';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
  f = new FormGroup({
    customerId: new FormControl(1, [Validators.required]),
    name: new FormControl(''),
    surname: new FormControl(''),
    initialCredit: new FormControl(0, [Validators.required])
  });

  constructor(private router: Router, private userService: UserService) { }

  ngOnInit(): void {
  }

  submit() {
    this.userService.add(this.f.value as CreateUserRequest);
    this.router.navigateByUrl('');
  }
}

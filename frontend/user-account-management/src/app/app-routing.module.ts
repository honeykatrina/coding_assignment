import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './components/user/account/account.component';
import { AddComponent } from './components/user/account/add/add.component';
import { UserComponent } from './components/user/user.component';

const routes: Routes = [
  { path: '', component: UserComponent },
  { path: 'accounts', component: AccountComponent },
  { path: 'accounts/add', component: AddComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

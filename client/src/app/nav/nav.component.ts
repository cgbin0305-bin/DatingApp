import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';

/* 
  when the component move away to another component => what ever stored  in memory for that component is gone 
  => we gonna using service because services only survive as long as our application is alive
*/
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {}; // the properties that store what user completed in form => initialize it in empty obj
  // loggedIn = false;
  // currentUser$: Observable<User | null> = of(null); change the constructor to public then use currentUser$ in account service

  constructor(public accountService: AccountService) {}

  ngOnInit(): void {
    // this.getCurrentUser();
    // this.currentUser$ = this.accountService.currentUser$;
  }

  // getCurrentUser() {
  //   // need to unsubscribe it (it can leak our data) by using async pipe template(it's will be automatically subscribe and unsubscribe)
  //   this.accountService.currentUser$.subscribe({
  //     next: (user) => (this.loggedIn = !!user), // turn user object type into boolean type by using !! => if have user return true
  //     error: (error) => console.log(error),
  //   }); // because it is an observable => need be subscribe
  //   console.log('LoggedId: ' + this.loggedIn);
  // }

  login() {
    this.accountService.login(this.model).subscribe({
      // when the login method is done, we are no longer subscribe it => not essential to unsubscribe it
      next: (response) => {
        console.log(response);
        // this.loggedIn = true;
      },
      error: (error) => console.log(error),
    });
  }

  logout() {
    this.accountService.logout();
    // this.loggedIn = false;
  }
}

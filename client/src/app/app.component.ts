import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';

/*
  ng g : ng is for access into Angular CLI and g is generate
*/
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title: string = 'Dating App';
  // need something to store users in
  users: any;

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    /*
     inside here we're going to make a req to our API server 
  */
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }
}

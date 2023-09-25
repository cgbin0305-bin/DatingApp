import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  /* 
  Account service is going to be responsible for making HTTP req from client to server
  When our app is initialized => services are also initialized at the same time => the service is not destroyed until our application is finished 
   
*/
  baseUrl = 'https://localhost:5000/api/';
  /*
    we gonna use a kind of observable called behavior subject => allow us to give an observable (an initial value) that can use elsewhere in our app
  */
  private currentUserSource = new BehaviorSubject<User | null>(null); // it can be user or can be null
  currentUser$ = this.currentUserSource.asObservable(); //$ is a convention to signify that this is unobservable

  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      //because this is post request => send the param into body of req.
      map((response: User) => {
        // inside map operator we can pass function or we can do something with the response
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user)); // local storage just store a pair of key value not store a object => turn json format into a string (JSON.stringify)
          this.currentUserSource.next(user);
        }
      })
    );
  }
  register(model: any) {
    // using MAP method, if want to return value   => must return in map method
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map((user) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }
  logout() {
    // remove item from local storage
    localStorage.removeItem('user'); // use key to remove
    this.currentUserSource.next(null);
  }
}

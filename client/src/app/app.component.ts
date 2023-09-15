import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title: string = 'Dating App';
  // need something to store users in
  users: any;
  constructor(private http: HttpClient) {}
  ngOnInit(): void {
    /*
     inside here we're going to make a req to our API server 
  */
    this.http.get('https://localhost:5000/api/users').subscribe({
      next: (response) => (this.users = response), // right now type of users is a list of users
      error: (error) => console.log(error), //callback function to do something wrong (Write log into console went wrong  )
      complete: () => console.log('Request has completed'),
    });
  }
}

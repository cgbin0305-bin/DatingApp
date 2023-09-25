import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }
  getUsers() {
    this.http.get('https://localhost:5000/api/users').subscribe({
      next: (response) => (this.users = response), // right now type of users is a list of users
      error: (error) => console.log(error), //callback function to do something wrong (Write log into console went wrong  )
      complete: () => console.log('Request has completed'),
    });
  }
  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}

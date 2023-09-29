import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  // @Input() usersFromHomeComponent: any; // tell the registerComponent to receive some data/info from home component (Parent to Child communication)
  @Output() cancelRegister = new EventEmitter(); // emit an event from register component to home component (Child to parent communication)
  model: any = {};

  constructor(
    private accountService: AccountService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {}

  register() {
    this.accountService.register(this.model).subscribe({
      next: () => {
        this.cancel();
      },
      error: (error) => this.toastr.error(error.error),
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}

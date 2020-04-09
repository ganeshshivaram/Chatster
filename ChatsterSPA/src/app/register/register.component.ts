import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { AuthService } from 'src/_services/auth.service';
import { AlertifyService } from 'src/_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerModel: any = {};
  @Output() cancelRegister = new EventEmitter();
  constructor(
    private authService: AuthService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {}

  register() {
    this.authService.register(this.registerModel).subscribe(
      () => {
        this.alertify.success('Registration successful');
      },
      error => {
        this.alertify.error(error);
      }
    );
  }

  cancelRegistration() {
    this.cancelRegister.emit(false);
    this.alertify.warning('cancelled');
  }
}

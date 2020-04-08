import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { AuthService } from 'src/_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerModel: any = {};
  @Output() cancelRegister = new EventEmitter();
  constructor(private authService: AuthService) {}

  ngOnInit() {}

  register() {
    this.authService.register(this.registerModel).subscribe(
      () => {
        console.log('Registration successful');
      },
      (error) => {
        console.log(error);
      }
    );
  }

  cancelRegistration() {
    this.cancelRegister.emit(false);
    console.log('cancelled');
  }
}

import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  isRegisterMode = false;

  constructor() { }

  ngOnInit() {
  }

  setRegistrationMode() {
    this.isRegisterMode = true;
  }

  cancelRegistration(bCancelRegister) {
    this.isRegisterMode = bCancelRegister;
  }

}

import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/_models/user';
import { UserService } from 'src/_services/user.service';
import { AuthService } from 'src/_services/auth.service';
import { AlertifyService } from 'src/_services/alertify.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css'],
})
export class MemberCardComponent implements OnInit {
  @Input() member: User;
  constructor(
    private userService: UserService,
    private authService: AuthService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {}

  sendLike(recipientId: number) {
    const userId = this.authService.loggedInUser.id;
    this.userService.sendLike(userId, recipientId).subscribe(
      () => {
        this.alertify.success(
          'Successfully liked user: ' + this.member.knownAs
        );
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }
}

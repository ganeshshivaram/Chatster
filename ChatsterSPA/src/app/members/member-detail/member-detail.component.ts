import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from 'src/_services/user.service';
import { AlertifyService } from 'src/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/_models/user';
import {
  NgxGalleryOptions,
  NgxGalleryImage,
  NgxGalleryAnimation,
} from '@kolkov/ngx-gallery';
import { TabsetComponent } from 'ngx-bootstrap/tabs/public_api';
import { AuthService } from 'src/_services/auth.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css'],
})
export class MemberDetailComponent implements OnInit {
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  @ViewChild('memberTabSet', { static: true }) memberTabSet: TabsetComponent;

  constructor(
    private userService: UserService,
    private alertify: AlertifyService,
    private authService: AuthService,
    private route: ActivatedRoute
  ) {}

  user: User;
  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.user = data.user;
      this.galleryImages = this.getImages();
    });

    this.route.queryParams.subscribe((queryParams) => {
      const selectedTab = queryParams['tab'];
      if (selectedTab != null) {
        this.selectTab(selectedTab);
      }
    });

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        imagePercent: 100,
        preview: false,
      },
    ];

    this.galleryImages = this.getImages();
  }

  selectTab(tabId: number) {
    this.memberTabSet.tabs[tabId].active = true;
  }

  sendLike(recipientId: number) {
    const userId = this.authService.loggedInUser.id;
    this.userService.sendLike(userId, recipientId).subscribe(
      () => {
        this.alertify.success('Successfully liked user: ' + this.user.knownAs);
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }

  getImages() {
    const imageUrls = [];

    for (const photo of this.user.photos) {
      imageUrls.push({
        small: photo.url,
        medium: photo.url,
        big: photo.url,
      });
    }
    return imageUrls;
  }
}

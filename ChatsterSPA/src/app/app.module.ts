import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { JwtModule } from '@auth0/angular-jwt';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';

import { AuthService } from 'src/_services/auth.service';
import { HttpInterceptorProvider } from 'src/_services/error.interceptor';
import { AlertifyService } from 'src/_services/alertify.service';
import { MemberDetailResolver } from 'src/_resolvers/member-detail.resolver';

import { RoutesRoutes } from 'src/routes.routing';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MembersResolver } from 'src/_resolvers/members.resolver';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    MemberCardComponent,
    MessagesComponent,
    ListsComponent,
    MemberDetailComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    RoutesRoutes,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth'],
      },
    }),
    TabsModule.forRoot(),
    NgxGalleryModule,
  ],
  providers: [
    AuthService,
    HttpInterceptorProvider,
    AlertifyService,
    MemberDetailResolver,
    MembersResolver,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

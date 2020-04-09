import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

import { AuthService } from 'src/_services/auth.service';
import { HttpInterceptorProvider } from 'src/_services/error.interceptor';
import { AlertifyService } from 'src/_services/alertify.service';

import { RoutesRoutes } from 'src/routes.routing';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { FriendsListComponent } from './friends-list/friends-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    FriendsListComponent,
    MessagesComponent,
    ListsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    RoutesRoutes
  ],
  providers: [AuthService, HttpInterceptorProvider, AlertifyService],
  bootstrap: [AppComponent]
})
export class AppModule {}

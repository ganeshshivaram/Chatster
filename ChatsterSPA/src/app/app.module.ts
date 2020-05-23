import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { JwtModule } from '@auth0/angular-jwt';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxGalleryModule } from 'ngx-gallery';
import { FileUploadModule } from 'ng2-file-upload';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { TimeAgoPipe } from 'time-ago-pipe';

import { AuthService } from 'src/_services/auth.service';
import { HttpInterceptorProvider } from 'src/_services/error.interceptor';
import { AlertifyService } from 'src/_services/alertify.service';
import { MemberDetailResolver } from 'src/_resolvers/member-detail.resolver';
import { MembersResolver } from 'src/_resolvers/members.resolver';
import { MemberEditResolver } from 'src/_resolvers/member-edit.resolver';

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
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { PhotoEditorComponent } from './photo/photo-editor/photo-editor.component';
import { ListResolver } from 'src/_resolvers/list.resolver';
import { MessagesResolver } from 'src/_resolvers/messages.resolver';
import { MemberMessagesComponent } from './members/member-messages/member-messages.component';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    TimeAgoPipe,
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    MemberCardComponent,
    MessagesComponent,
    ListsComponent,
    MemberDetailComponent,
    MemberEditComponent,
    PhotoEditorComponent,
    MemberMessagesComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot(),
    ButtonsModule.forRoot(),
    RoutesRoutes,
    JwtModule.forRoot({
      config: {
        tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        // tslint:disable-next-line: object-literal-sort-keys
        blacklistedRoutes: ['localhost:5000/api/auth'],
      },
    }),
    TabsModule.forRoot(),
    NgxGalleryModule,
    FileUploadModule,
    ReactiveFormsModule,
  ],
  providers: [
    AuthService,
    HttpInterceptorProvider,
    AlertifyService,
    MemberDetailResolver,
    MembersResolver,
    MemberEditResolver,
    ListResolver,
    MessagesResolver,
  ],
  // tslint:disable-next-line: object-literal-sort-keys
  bootstrap: [AppComponent],
})
export class AppModule {}

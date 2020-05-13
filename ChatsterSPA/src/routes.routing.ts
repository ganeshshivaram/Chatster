import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './app/home/home.component';
import { MemberListComponent } from './app/members/member-list/member-list.component';
import { MessagesComponent } from './app/messages/messages.component';
import { ListsComponent } from './app/lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailComponent } from './app/members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MembersResolver } from './_resolvers/members.resolver';
import { MemberEditComponent } from './app/members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { MemberUnsavedChangesGuard } from './_guards/member-unsaved-changes.gaurd';
import { ListResolver } from './_resolvers/list.resolver';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'members',
        component: MemberListComponent,
        resolve: { users: MembersResolver },
      },
      {
        path: 'members/:id',
        component: MemberDetailComponent,
        resolve: { user: MemberDetailResolver },
      },
      {
        path: 'member/edit',
        component: MemberEditComponent,
        resolve: { user: MemberEditResolver },
        canDeactivate: [MemberUnsavedChangesGuard],
      },
      { path: 'messages', component: MessagesComponent },
      {
        path: 'lists',
        component: ListsComponent,
        resolve: { users: ListResolver },
      },
    ],
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];

export const RoutesRoutes = RouterModule.forRoot(routes);

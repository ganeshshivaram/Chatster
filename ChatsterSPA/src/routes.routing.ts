import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './app/home/home.component';
import { FriendsListComponent } from './app/friends-list/friends-list.component';
import { MessagesComponent } from './app/messages/messages.component';
import { ListsComponent } from './app/lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'friends', component: FriendsListComponent },
      { path: 'messages', component: MessagesComponent },
      { path: 'lists', component: ListsComponent }
    ]
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full' }
];

export const RoutesRoutes = RouterModule.forRoot(routes);

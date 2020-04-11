import { Routes } from '@angular/router';
import { HomeComponent } from '@app/home/home.component';
import { PrivacyComponent } from '@app/components';
import { ContactComponent } from '@app/contact/contact.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full', data: { state: 'home' } },
  { path: 'contact', component: ContactComponent, pathMatch: 'full', data: { state: 'contact' } },
  { path: 'examples', loadChildren: () => import('./+examples/examples.module').then(m => m.ExamplesModule) },
  { path: 'user', loadChildren: () => import('./user/user.module').then(m => m.UsersModule) },
  { path: 'privacy', component: PrivacyComponent }
];

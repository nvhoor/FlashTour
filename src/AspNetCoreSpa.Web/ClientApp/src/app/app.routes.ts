import { Routes } from '@angular/router';
import { HomeComponent } from '@app/home/home.component';
import { PrivacyComponent } from '@app/components';
import { ContactComponent } from '@app/contact/contact.component';
import {AdminComponent} from "@app/admin/admin.component";
import {DashboardComponent} from "@app/admin/dashboard/dashboard.component";
import {UserComponent} from "@app/user/user.component";
import {HomeDetailComponent} from "@app/user/home-detail/home-detail.component";
import {DetailComponent} from "@app/user/detail/detail.component";
import {TourBookingComponent} from "@app/user/bookings/tour-booking.component";
import {AutoLoginComponent} from "@app/user/auto-login/auto-login.component";
import {ManageToursComponent} from "@app/admin/manage-tours/manage-tours.component";
import {ManageTourBookingsComponent} from "@app/admin/manage-tour-bookings/manage-tour-bookings.component";
import {ManageTourCategoriesComponent} from "@app/admin/manage-tour-categories/manage-tour-categories.component";
import {ManageAccountsComponent} from "@app/admin/manage-accounts/manage-accounts.component";
import {ManagePostsComponent} from "@app/admin/manage-posts/manage-posts.component";

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full', data: { state: 'home' } },
  { path: 'contact', component: ContactComponent, data: { state: 'contact' } },
  { path: 'examples', loadChildren: () => import('./+examples/examples.module').then(m => m.ExamplesModule) },
  { path: 'user',component: UserComponent,children : [
          { path: '', redirectTo:"home-detail",pathMatch:"full" },
          { path: 'home-detail', component: HomeDetailComponent },
          { path: 'detail/:id', component: DetailComponent },
          { path: 'tour-booking/:id', component: TourBookingComponent },
          { path: 'auto-login', component: AutoLoginComponent }
      ] },
  { path: 'admin', component: AdminComponent,children : [
      { path: '', redirectTo:"dashboard",pathMatch:"full" },
      { path: 'dashboard', component: DashboardComponent },
          { path: 'manage-tours', component: ManageToursComponent },
          { path: 'manage-tour-bookings', component: ManageTourBookingsComponent },
          { path: 'manage-tour-categories', component: ManageTourCategoriesComponent },
          { path: 'manage-accounts', component: ManageAccountsComponent },
          { path: 'manage-posts', component: ManagePostsComponent }
    ]},
  { path: 'privacy', component: PrivacyComponent }
    
];

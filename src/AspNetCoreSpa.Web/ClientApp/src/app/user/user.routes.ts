import { Routes } from '@angular/router';

import {DetailComponent } from './detail/detail.component';
import {TourBookingComponent} from "@app/user/bookings/tour-booking.component";
//Khai báo một constant chứa các route của app
export const routes: Routes = [
    { path: 'detail/:id', component: DetailComponent },
    { path: 'tour-booking/:id', component: TourBookingComponent }
];

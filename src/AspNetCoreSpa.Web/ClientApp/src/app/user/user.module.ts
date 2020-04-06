import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '@app/shared';

import { DetailComponent } from './detail/detail.component';
import {UserComponent} from "./user.component";
import { routes } from './user.routes';
import {TourBookingComponent} from "@app/user/bookings/tour-booking.component";
import { FormsModule } from '@angular/forms';
@NgModule({
    imports: [
        FormsModule,
        SharedModule,
        RouterModule.forChild(routes)
    ],
    declarations: [DetailComponent,TourBookingComponent,UserComponent]
})
export class UsersModule { }


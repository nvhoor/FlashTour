import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '@app/shared';

import { DetailComponent } from './detail/detail.component';
import {UserComponent} from "./user.component";
import { routes } from './user.routes';
import {TourBookingComponent} from "@app/user/bookings/tour-booking.component";
import { FormsModule } from '@angular/forms';
import { BannerComponent } from './banner/banner.component';
import { HomeDetailComponent } from './home-detail/home-detail.component';
import { TourCategoryComponent } from './tour-category/tour-category.component';
@NgModule({
    imports: [
        FormsModule,
        SharedModule,
        RouterModule.forChild(routes)
    ],
    exports: [
        BannerComponent,
        HomeDetailComponent,
        DetailComponent
    ],
    declarations: [DetailComponent, TourBookingComponent, UserComponent, BannerComponent, HomeDetailComponent, TourCategoryComponent]
})
export class UsersModule { }


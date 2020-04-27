import { BrowserModule, BrowserTransferStateModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER, ErrorHandler } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ServiceWorkerModule } from '@angular/service-worker';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

// import { PrebootModule } from 'preboot';

import { environment } from '../environments/environment';

import { AppSharedModule } from './appshared';
import { ToastrModule } from './toastr';

import { routes } from './app.routes';
// Components
import { FooterComponent, HeaderComponent, ModalComponent, PrivacyComponent, ModalTemplateDirective } from '@app/components';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
// Services
import { AppService, AuthService, DataService, GlobalErrorHandler, ModalService, ModalStateService, AuthInterceptor, TimingInterceptor } from '@app/services';
import {ContactComponent} from "@app/contact/contact.component";
import {FormsModule} from "@angular/forms";
import {UsersModule} from "@app/user/user.module";
import {AdminComponent} from "@app/admin/admin.component";
import {DashboardComponent} from "@app/admin/dashboard/dashboard.component";
import {SharedModule} from "@app/shared";
import {DetailComponent} from "@app/user/detail/detail.component";
import {TourBookingComponent} from "@app/user/bookings/tour-booking.component";
import {UserComponent} from "@app/user/user.component";
import {BannerComponent} from "@app/user/banner/banner.component";
import {HomeDetailComponent} from "@app/user/home-detail/home-detail.component";
import {TourCategoryComponent} from "@app/user/tour-category/tour-category.component";
import {AutoLoginComponent} from "@app/user/auto-login/auto-login.component";
import {ManageToursComponent} from "@app/admin/manage-tours/manage-tours.component";
import {ManageTourBookingsComponent} from "@app/admin/manage-tour-bookings/manage-tour-bookings.component";
import {ManageTourCategoriesComponent} from "@app/admin/manage-tour-categories/manage-tour-categories.component";
import {ManageAccountsComponent} from "@app/admin/manage-accounts/manage-accounts.component";
import {ManagePostsComponent} from "@app/admin/manage-posts/manage-posts.component";
import {ManageContactsComponent} from "@app/admin/manage-contacts/manage-contacts.component";
import {ManageBannersComponent} from "@app/admin/manage-banners/manage-banners.component";
export function appServiceFactory(appService: AppService, authService: AuthService): Function {
  return () => appService.getAppData(authService);
}
@NgModule({
  declarations: [
    // Components
    AppComponent,
    HomeComponent,
    FooterComponent,
    HeaderComponent,
    ModalComponent,
    ModalTemplateDirective,
    PrivacyComponent,
    ContactComponent,
      AdminComponent,
      DashboardComponent,
      DetailComponent, TourBookingComponent, UserComponent, BannerComponent, HomeDetailComponent, TourCategoryComponent,AutoLoginComponent,
       ManageToursComponent, ManageTourBookingsComponent, ManageTourCategoriesComponent, ManageAccountsComponent, ManagePostsComponent,ManageContactsComponent,ManageBannersComponent
  ],
    imports: [
        BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
        // PrebootModule.withConfig({ appRoot: 'appc-root' }),
        BrowserAnimationsModule,
        BrowserTransferStateModule,
        HttpClientModule,
        AppSharedModule,
        // OAuthModule.forRoot(),
        NgbModule,
        ToastrModule.forRoot(),
        RouterModule.forRoot(routes, {initialNavigation: 'enabled'}),
        ServiceWorkerModule.register('ngsw-worker.js', {enabled: environment.production}),
        FormsModule,
        UsersModule,
        SharedModule
    ],
  providers: [
    AppService,
    AuthService,
    DataService,
    ModalService,
    ModalStateService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: TimingInterceptor, multi: true },
    { provide: APP_INITIALIZER, useFactory: appServiceFactory, deps: [AppService, AuthService], multi: true },
    { provide: ErrorHandler, useClass: GlobalErrorHandler }
  ],
    exports: [
        HeaderComponent,
        FooterComponent
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }

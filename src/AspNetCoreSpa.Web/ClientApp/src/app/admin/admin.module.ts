import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '@app/shared';
import { FormsModule } from '@angular/forms';
import {routes} from "@app/admin/admin.routes";
import {AdminComponent} from "@app/admin/admin.component";
import { ManageAccountsComponent } from './manage-accounts/manage-accounts.component';
import { ManagePostsComponent } from './manage-posts/manage-posts.component';
import { ManageContactsComponent } from './manage-contacts/manage-contacts.component';
import { ManageBannersComponent } from './manage-banners/manage-banners.component';
@NgModule({
    imports: [
        FormsModule,
        SharedModule,
        RouterModule.forChild(routes)
    ],
    exports: [
        
    ],
    declarations: []
})
export class AdminsModule { }
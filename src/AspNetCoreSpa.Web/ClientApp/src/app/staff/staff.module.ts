import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '@app/shared';
import { FormsModule } from '@angular/forms';
import {routes} from "@app/staff/staff.routes";
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
export class StaffsModule { }
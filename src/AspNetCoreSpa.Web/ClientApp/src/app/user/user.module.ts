import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '@app/shared';
import { routes } from './user.routes';
import { FormsModule } from '@angular/forms';
@NgModule({
    imports: [
        FormsModule,
        SharedModule,
        RouterModule.forChild(routes)
    ],
    exports: [
    ],
})
export class UsersModule { }


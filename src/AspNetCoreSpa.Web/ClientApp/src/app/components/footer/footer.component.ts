import { Component } from '@angular/core';
import {AppService, AuthService} from "@app/services";
import {User} from "oidc-client";

@Component({
    selector: 'appc-footer',
    styleUrls: ['./footer.component.scss'],
    templateUrl: './footer.component.html'
})
export class FooterComponent {
    constructor(
        private authService: AuthService,
    ) { }
    get user(): User {
        return this.authService.user;

    }
    get IsNotUser():boolean{
        var isNotUser=false;
        try{
            isNotUser = this.authService.user.profile.role.some(x=>{
                return x=="admin"||x=="Admin" ||x=="staff"||x=="Staff"
            });
        }catch (e) {
            isNotUser=false;
        }
        return isNotUser;
    }
}

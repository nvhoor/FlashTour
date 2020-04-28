import { Component, OnInit } from '@angular/core';
import {ActivatedRouteSnapshot, Route, Router, RouterStateSnapshot} from "@angular/router";
import {Observable} from "rxjs";
import {AuthService} from "@app/services";
@Component({
    selector: 'appc-admin',
    templateUrl: './admin.component.html',
})
export class AdminComponent implements OnInit {

   
    constructor(private authService: AuthService,private router: Router) {}
    ngOnInit() {
        if(!this.authService.getIsAdmin()){
            this.router.navigate([""]);
        }
    }

}

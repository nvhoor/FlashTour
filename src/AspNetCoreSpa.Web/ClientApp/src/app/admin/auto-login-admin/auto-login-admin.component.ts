import { Component, OnInit } from '@angular/core';
import {AuthService} from "@app/services";
import {Router} from "@angular/router";
import {User} from "oidc-client";

@Component({
  selector: 'appc-auto-login-admin',
  templateUrl: './auto-login-admin.component.html',
  styleUrls: ['./auto-login-admin.component.scss']
})
export class AutoLoginAdminComponent implements OnInit {

  constructor(private authService: AuthService,
              private router:Router) { }

  ngOnInit() {
    if(!this.authService.isLoggedIn()){
      this.login();
    }
    if(this.IsNotUser){
      this.router.navigate(["admin"]);
    }else{
      this.router.navigate([""]);
    }
  }
  login() {
    this.authService.loginSilent().then((user)=>{
      window.location.href = "https://localhost:5005/admin";
    });
  }
  get user(): User {
    return this.authService.user;

  }
  get IsNotUser():boolean{
    var isNotUser=false;
    try{
      isNotUser = this.authService.user.profile.role.some(x=>{
        return x=="admin"||x=="Admin" ||x=="staff"||x=="Staff";
      });
    }catch (e) {
      isNotUser=false;
    }
    return isNotUser;
  }
  getIsAdmin():boolean{
    var isAdmin=false;
    try{
      isAdmin = this.authService.user.profile.role.some(x=>{
        return x=="admin"||x=="Admin"
      });
    }catch (e) {
      isAdmin=false;
    }
    return isAdmin;
  }
  getIsStaff():boolean{
    var isStaff=false;
    try{
      isStaff = this.authService.user.profile.role.some(x=>{
        return x=="staff"||x=="Staff"
      });
    }catch (e) {
      isStaff=false;
    }
    return isStaff;
  }

}

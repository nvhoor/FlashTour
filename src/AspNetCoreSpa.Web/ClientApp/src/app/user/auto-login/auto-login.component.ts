import { Component, OnInit } from '@angular/core';
import {AuthService} from "@app/services";
import { Router} from "@angular/router";

@Component({
  selector: 'appc-auto-login',
  templateUrl: './auto-login.component.html',
  styleUrls: ['./auto-login.component.scss']
})
export class AutoLoginComponent implements OnInit {

  constructor(private authService: AuthService,
              private router:Router) { }

  ngOnInit() {
    if(!this.authService.isLoggedIn()){
      this.login();
    }
    this.router.navigate([""]);
  }
  login() {
    this.authService.login();
  }
}

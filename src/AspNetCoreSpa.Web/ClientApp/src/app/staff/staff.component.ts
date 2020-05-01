import { Component, OnInit } from '@angular/core';
import {AuthService} from "@app/services";
import {Router} from "@angular/router";

@Component({
  selector: 'appc-staff',
  templateUrl: './staff.component.html',
  styleUrls: ['./staff.component.scss']
})
export class StaffComponent implements OnInit {

  constructor(private authService: AuthService,private router: Router) {}
  ngOnInit() {
    if(!this.authService.getIsStaff()){
      this.router.navigate([""]);
    }
  }

}

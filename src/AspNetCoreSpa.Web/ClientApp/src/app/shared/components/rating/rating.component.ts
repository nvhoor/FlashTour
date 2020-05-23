import {Component, Input, OnInit} from '@angular/core';
import {AuthService, DataService} from "@app/services";
import {User} from "oidc-client";
import {Router} from "@angular/router";

@Component({
  selector: 'appc-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.scss']
})
export class RatingComponent implements OnInit {
  public currentRate = 0;
  @Input() evaluation:Evaluation;
  constructor(private authService: AuthService,
              private router:Router,
  private _dataService:DataService,) { }

  ngOnInit() {
  }

  rateTourChange() {
    // if(!this.authService.isLoggedIn()){
    //   let accept=confirm("You have to login to evaluate this tour?");
    //   if(accept){
    //     this.login();
    //   }
    // }else{
    //   if(this.IsNotUser){
    //     // this._dataService.put<Evaluation>(`api/Evaluation/${this.evaluation.id}`,{...this.evaluation}).subscribe(x=>{
    //     //   console.log("Update evaluation success!");
    //     // },error => {  console.error(error);});
    //     console.log("rateTourChange",this.currentRate,JSON.stringify(this.evaluation));
    //   }
    // }
    let accept=confirm("Are you warn to evaluate this tour "+this.currentRate+" star?");
      if(accept){
        this._dataService.put<Evaluation>(`api/Evaluation/${this.evaluation.id}`,this.currentRate).subscribe(x=>{
          console.log("Rate tour success!");
          switch (this.currentRate)
          {
            case 1:
              this.evaluation.oneStar += 1;
              break;
            case 2:
              this.evaluation.twoStar += 1;
              break;
            case 3:
              this.evaluation.threeStar += 1;
              break;
            case 4:
              this.evaluation.fourStar += 1;
              break;
            case 5:
              this.evaluation.fiveStar += 1;
              break;
          }
        },error => {  console.error(error);});
        console.log("rateTourChange",this.currentRate,JSON.stringify(this.evaluation));
      }
  }
  login() {
    this.authService.loginSilent();
    if(this.IsNotUser){
      this.router.navigate(["admin"]);
    }
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
}


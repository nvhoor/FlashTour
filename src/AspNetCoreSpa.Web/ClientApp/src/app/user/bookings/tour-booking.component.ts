import {Component, Inject, Input, OnInit, Renderer2, ViewChild} from '@angular/core';
import {DOCUMENT} from "@angular/common";
import {DataService} from "@app/services";
import {ActivatedRoute} from '@angular/router';
import {AppFormComponent, FormsService} from "@app/shared";
import {IFieldConfig} from "@app/models";

@Component({
    selector: 'appc-user-tour-booking-component',
    templateUrl: './tour-booking.component.html',
    styleUrls:['./tour-booking.component.scss']
})
export class TourBookingComponent implements OnInit{
    public tour:Tour;
    public listCustomer:Customer[];
    public totalCustomer=1;
    public totalValue=0;
    public timerAlertOutOfSlot=true;
    public timeOutGenerateListCustomer=false;
    @ViewChild(AppFormComponent, { static: true }) form: AppFormComponent;
    config: IFieldConfig[];
    @Input() comunication:Comunication;
        constructor(
            @Inject("BASE_URL") private baseUrl: string,
            private route: ActivatedRoute,
            private _renderer2: Renderer2,
            private _dataService:DataService,
            private formsService: FormsService,
        @Inject(DOCUMENT) private _document: Document
    ) {
        }
    public ngOnInit() {
            var id=this.route.snapshot.params['id'];
            this.comunication={
            fullName:"",
            address:"",
            email:"",
            note:"",
            mobile:"",
            adult:1,
            child:0,
            kid:0,
                tourId:id,
                tourCustomers:[],
                bookingPrices:[]
        };
        this.totalCustomer=this.comunication.adult+this.comunication.child+this.comunication.kid;
        this.getTourById(id);
        this.initListCustomer()
    }
    
    private getTourById(id: any) {
        var data = this._dataService.get<Tour>(`${this.baseUrl}api/Tour/${id}`);
        let that = this;
        data.subscribe((result) => {
            that.tour = result;
            let date1 = new Date(that.tour.departureDate);
            let date2 = new Date(that.tour.tourPrograms[that.tour.tourPrograms.length-1].date);
            // @ts-ignore
            let diffTime = Math.abs(date2 - date1);
            let diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
            that.tour.daysLeft=diffDays==1?diffDays+" day":diffDays+" days";
            console.log("Tour detail:"+JSON.stringify(that.tour));
        }, error => console.error(error));
        
    }

    getTotal(elementId) {
            var total=this.comunication.adult+this.comunication.child+this.comunication.kid;
            if(total>this.tour.slot){
                if(this.timerAlertOutOfSlot){
                    this.timerAlertOutOfSlot=false;
                    alert(`The number of seats remaining for this tour is ${this.tour.slot}. You need to re-select the number of guests`); 
                    setTimeout(()=>{this.timerAlertOutOfSlot=true},1000);
                }
                this._document.getElementById(elementId).classList.add("");
                return this.totalCustomer;
            }
        this.totalCustomer=total;
        this.initListCustomer(); 
        return this.totalCustomer;
    }
    initListCustomer(){
        if(!this.timeOutGenerateListCustomer){
            this.timeOutGenerateListCustomer=true;
          setTimeout(()=>{
              if(this.totalCustomer<=this.tour.slot) {
                  this.listCustomer = [];
                  this.totalValue=0;
                  for (let i = 0; i < this.comunication.adult; i++) {
                      this.listCustomer.push({
                          id:"",
                          tourBookingId:"",
                          fullName: "",
                          gender: false,
                          birthDay: new Date(),
                          touristType: 0,
                          value: this.getPrice(this.tour.prices[0])
                      });
                      this.totalValue += this.getPrice(this.tour.prices[0]);
                  }
                  console.log("getPrice",this.getPrice(this.tour.prices[0]));
                  for (let j = 0; j < this.comunication.child; j++) {
                      this.listCustomer.push({
                          id:"",
                          tourBookingId:"",
                          fullName: "",
                          gender: false,
                          birthDay: new Date(),
                          touristType: 1,
                          value: this.getPrice(this.tour.prices[1])
                      });
                      this.totalValue += this.getPrice(this.tour.prices[1]);
                  }
                  for (let k = 0; k < this.comunication.kid; k++) {
                      this.listCustomer.push({
                          id:"",
                          tourBookingId:"",
                          fullName: "",
                          gender: false,
                          birthDay: new Date(),
                          touristType: 2,
                          value: this.getPrice(this.tour.prices[2])
                      });
                      
                      this.totalValue += this.getPrice(this.tour.prices[2]);
                  }
              }
              this.timeOutGenerateListCustomer=false;
          },2000);
        }  
    }
    postTourBooking(){
        let ok=confirm("Are you sure to booking this tour?");
        if(ok) {
            var prices=[];
            this.comunication.bookingPrices=prices;
            this.comunication.tourCustomers=this.listCustomer;
            console.log("Post tour booking:", JSON.stringify(this.comunication));
            this._dataService.post<Comunication>(`${this.baseUrl}api/TourBooking`, JSON.stringify(this.comunication)).subscribe(x => {
                alert("Book tour success!");
            }, error => {
                alert("Book tour fail!");
                console.error(error);
            });
        }
    }
    newGuid() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
            var r = Math.random() * 16 | 0,
                v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }
    checkExpiredPromotion(price) {
        var toCheck = new Date().getTime();
        var startDatePro = new Date(price.startDatePro).getTime();
        var endDatePro = new Date(price.endDatePro).getTime();
        return toCheck >= startDatePro && toCheck <= endDatePro;
    }
    getPrice(price) {
        if (this.checkExpiredPromotion(price)) {
            return price.promotionPrice;
        } else {
            return price.originalPrice;
        }
    }
}
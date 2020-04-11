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
        @Inject(DOCUMENT) private _document: Document,
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
            let date1 = new Date();
            let date2 = new Date(that.tour.departureDate);
            // @ts-ignore
            let diffTime = Math.abs(date2 - date1);
            let diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
            that.tour.daysLeft=diffDays==1?diffDays+" day":diffDays+" days";
            console.log("Tour detail:"+JSON.stringify(that.tour));
        }, error => console.error(error));
        
    }

    getTotal() {
        this.totalCustomer=this.comunication.adult+this.comunication.child+this.comunication.kid;
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
                          fullName: "",
                          gender: false,
                          birthday: new Date(),
                          touristType: 0,
                          value:this.tour.prices[0].promotionPrice
                      });
                      this.totalValue+=this.tour.prices[0].promotionPrice;
                  }
                  for (let j = 0; j < this.comunication.child; j++) {
                      this.listCustomer.push({
                          fullName: "",
                          gender: false,
                          birthday: new Date(),
                          touristType: 1,
                          value:this.tour.prices[1].promotionPrice
                      });
                      this.totalValue+=this.tour.prices[1].promotionPrice;
                  }
                  for (let k = 0; k < this.comunication.kid; k++) {
                      this.listCustomer.push({
                          fullName: "",
                          gender: false,
                          birthday: new Date(),
                          touristType: 2,
                          value:this.tour.prices[2].promotionPrice
                      });
                      this.totalValue+=this.tour.prices[2].promotionPrice;
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
            for(let i=0;i<3;i++){
                prices.push({
                    touristType:i,
                    price:this.tour.prices[i].promotionPrice
                });
            }
            this.comunication.bookingPrices=prices;
            this.comunication.tourCustomers=this.listCustomer;
            console.log("Post tour booking:", JSON.stringify(this.comunication));
            this._dataService.post<Comunication>(`${this.baseUrl}api/TourBooking`, JSON.stringify(this.comunication)).subscribe(x => {
                console.log("Book tour success!");
            }, error => {
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
}
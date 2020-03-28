import {AfterContentInit, AfterViewInit, Component, Inject, OnInit, Renderer2} from '@angular/core';
import {DOCUMENT} from "@angular/common";
import {DataService} from "@app/services";

@Component({
  selector: 'appc-home-component',
  templateUrl: './home.component.html',
  styleUrls:['./home.component.scss']
})

export class HomeComponent implements OnInit{
 public hotestTours:Tour[];
 public newestTours:Tour[];
  constructor(
      @Inject("BASE_URL") private baseUrl: string,
      private _renderer2: Renderer2,
      private _dataService:DataService,
      @Inject(DOCUMENT) private _document: Document
  ) {}
  public ngOnInit() {
      this.getHostestTour();
      this.getNewsetTour();
      setTimeout(function () {
          this.appendScriptCountDown();
      }.bind(this),5000);
  }
  private appendScriptCountDown() {
      console.log("lenght:"+this.hotestTours.length);
      this.hotestTours.forEach(function (tour) {
          let script = this._renderer2.createElement('script');
          script.type = `text/javascript`;
          script.text = `
            {
                 var countDownDate = new Date("${tour.departureDate}").getTime();
                  var x = setInterval(function() {
                    var now = new Date().getTime();
                    var distance = countDownDate - now;
                    var days = Math.floor(distance / (1000 * 60 * 60 * 24));
                    var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                    var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
                    var seconds = Math.floor((distance % (1000 * 60)) / 1000);
                    document.getElementById("count_${tour.id}").innerHTML = days + " days " + hours + "h "
                            + minutes + "m " + seconds + "s ";
                    if (distance < 0) {
                      clearInterval(x);
                      document.getElementById("count_${tour.id}").innerHTML = "EXPIRED";
                    }
                  }, 1000);
            }
        `;
          this._renderer2.appendChild(this._document.getElementById(tour.id), script); 
      }.bind(this));
  }

  private getHostestTour() {
    var data=this._dataService.getFull<Tour[]>(`${this.baseUrl}api/Tour`);
    let that=this;
    data.subscribe(function(result){
      // console.log("Respone:"+result.body);
      let hotestTours=[];
      result.body.forEach(function (d) {
        hotestTours.push({
          id:d.id,
          name:d.name,
          image:d.image,
          images:d.images,
          description:d.description,
          departureDate:d.departureDate,
          departureId:d.departureId,
          slot:d.slot,
          censorship:d.censorship,
          status:d.status,
          delete:d.delete,
          tourCategoryId:d.tourCategoryId,
          categoryName:null,
          tourCategory:null,
          evaluations:[]
        });
      });
      that.hotestTours=hotestTours;
      console.log(that.hotestTours);
    }, error => console.error(error));
  }

  private getNewsetTour() {
    var data=this._dataService.getFull<Tour[]>(`${this.baseUrl}api/Tour`);
    let that=this;
    data.subscribe(function(result){
      // console.log("Respone:"+result.body);
      let newestTours=[];
      result.body.forEach(function (d) {
         newestTours.push({
          id:d.id,
          name:d.name,
          image:d.image,
          images:d.images,
          description:d.description,
          departureDate:d.departureDate,
          departureId:d.departureId,
          slot:d.slot,
          censorship:d.censorship,
          status:d.status,
          delete:d.delete,
          tourCategoryId:d.tourCategoryId,
          categoryName:null,
          tourCategory:null,
          evaluations:[]
        });
      });
      that.newestTours=newestTours;
      console.log(that.newestTours);
    }, error => console.error(error));
  }
}
interface Tour {
  id:string,
  name:string,
  image:string,
  images:string,
  description:string,
  departureDate:Date,
  departureId:string,
  slot:number,
  censorship:boolean,
  status:boolean,
  delete:boolean,
  tourCategoryId:string,
  categoryName:null,
  tourCategory:null,
  evaluations:[]
}
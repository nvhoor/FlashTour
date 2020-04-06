import {Component, Inject, OnInit, Renderer2} from '@angular/core';
import {DOCUMENT} from "@angular/common";
import {DataService} from "@app/services";
import {CanDeactivate} from "@angular/router";
declare var $: any;
@Component({
    selector: 'appc-home-component',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit,CanDeactivate<HomeComponent>{
    
    public hotestTours: Hotest[];
    public newestTours: Newest[];
    public tourCatePagings: TourCatePagings[];
    public banners: Banner[];
    public isCountDown:boolean;
    constructor(
        @Inject("BASE_URL") private baseUrl: string,
        private _renderer2: Renderer2,
        private _dataService: DataService,
        @Inject(DOCUMENT) private _document: Document,
    ) { }

    public ngOnInit() {
        this.getHostestTour();
        this.getNewsetTour();
        this.getTourCategories();
        this.getBanners();
        this.isCountDown=false;
        var timer=setTimeout(() => {
            this.appendScriptCountDown();
            this.isCountDown=true;
        }, 5000);
        $('.carousel').carousel({
            interval: 5000
        })
    }
    canDeactivate(component: HomeComponent, currentRoute: import("@angular/router").ActivatedRouteSnapshot, currentState: import("@angular/router").RouterStateSnapshot, nextState?: import("@angular/router").RouterStateSnapshot): boolean | import("@angular/router").UrlTree | import("rxjs").Observable<boolean | import("@angular/router").UrlTree> | Promise<boolean | import("@angular/router").UrlTree> {
       
           throw new Error("Error Home canDeactivate.");  
       
    }
    private appendScriptCountDown() {
        console.log("lenght:" + this.hotestTours.length);
        this.hotestTours.forEach((tour) => {
            let script = this._renderer2.createElement('script');
            script.type = `text/javascript`;
            let id = tour.id.replace(/-/g, "");
            // console.log(id);
            script.text = `
            {
                 let countDownDateHot${id} = new Date("${tour.departureDate}").getTime();
                  let xHot${id} = setInterval(function() {
                    let nowHot${id} = new Date().getTime();
                    let distanceHot${id} = countDownDateHot${id} - nowHot${id};
                    let daysHot${id} = Math.floor(distanceHot${id} / (1000 * 60 * 60 * 24));
                    let hoursHot${id} = Math.floor((distanceHot${id} % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                    let minutesHot${id} = Math.floor((distanceHot${id} % (1000 * 60 * 60)) / (1000 * 60));
                    let secondsHot${id} = Math.floor((distanceHot${id} % (1000 * 60)) / 1000);
                    try{
                    document.getElementById("hotest_${tour.id}").innerHTML = daysHot${id} + " days " + hoursHot${id} + "h "
                            + minutesHot${id} + "m " + secondsHot${id} + "s ";
                            }catch(e){
                            clearInterval(xHot${id});
                            }
                    if (distanceHot${id} < 0) {
                      clearInterval(xHot${id});
                      document.getElementById("hotest_${tour.id}").innerHTML = "EXPIRED";
                    }
                  }, 1000);
            }
        `;
            var parentEle=this._document.getElementById("parent_hotest_" + tour.id);
           parentEle&&this._renderer2.appendChild(parentEle, script);
        });
        console.log("lenght:" + this.newestTours.length);
        this.newestTours.forEach((tour) => {
            let script = this._renderer2.createElement('script');
            script.type = `text/javascript`;
            let id = tour.id.replace(/-/g, "");
            // console.log(id);
            script.text = `
            {
                 let countDownDatenew${id} = new Date("${tour.departureDate}").getTime();
                  let xnew${id} = setInterval(function() {
                    let nownew${id} = new Date().getTime();
                    let distancenew${id} = countDownDatenew${id} - nownew${id};
                    let daysnew${id} = Math.floor(distancenew${id} / (1000 * 60 * 60 * 24));
                    let hoursnew${id} = Math.floor((distancenew${id} % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                    let minutesnew${id} = Math.floor((distancenew${id} % (1000 * 60 * 60)) / (1000 * 60));
                    let secondsnew${id} = Math.floor((distancenew${id} % (1000 * 60)) / 1000);
                    try{
                    document.getElementById("newest_${tour.id}").innerHTML = daysnew${id} + " days " + hoursnew${id} + "h "
                            + minutesnew${id} + "m " + secondsnew${id} + "s ";
                            }catch(e){
                            clearInterval(xnew${id});
                            }
                    if (distancenew${id} < 0) {
                      clearInterval(xnew${id});
                      document.getElementById("newest_${tour.id}").innerHTML = "EXPIRED";
                    }
                  }, 1000);
            }
        `;
            var parentEle=this._document.getElementById("parent_newest_" + tour.id);
            parentEle&&this._renderer2.appendChild(parentEle, script);
        });
    }

    private getHostestTour() {
        var data = this._dataService.getFull<HotestGroup[]>(`${this.baseUrl}api/Tour/hotest`);
        let that = this;
        data.subscribe((result) => {
            // console.log("Respone:"+result.body);
            let hotestTours = [];
            result.body.forEach((d) => {
                hotestTours.push({
                    id: d.tours[0].id,
                    name: d.tours[0].name,
                    image: d.tours[0].image,
                    description: d.tours[0].description,
                    departureDate: d.tours[0].departureDate,
                    departureId: d.tours[0].departureId,
                    slot: d.tours[0].slot,
                    originalPrice: d.tours[0].originalPrice,
                    promotionPrice: d.tours[0].promotionPrice,
                    startDatePro: d.tours[0].startDatePro,
                    touristType: d.tours[0].touristType,
                });
            });
            that.hotestTours = hotestTours;
            console.log(that.hotestTours);
        }, error => console.error(error));
    }

    private getNewsetTour() {
        var data = this._dataService.getFull<Newest[]>(`${this.baseUrl}api/Tour/newest`);
        let that = this;
        data.subscribe((result) => {
            // console.log("Respone:"+result.body);
            let newestTours = [];
            result.body.forEach((d) => {
                newestTours.push({
                    id: d.id,
                    name: d.name,
                    image: d.image,
                    description: d.description,
                    departureDate: d.departureDate,
                    departureId: d.departureId,
                    slot: d.slot,
                    originalPrice: d.originalPrice,
                    promotionPrice: d.promotionPrice,
                    startDatePro: d.startDatePro,
                    touristType: d.touristType,
                });
            });
            that.newestTours = newestTours;
            console.log(that.newestTours);
        }, error => console.error(error));
    }

    private getTourCategories() {
        var data = this._dataService.getFull<Newest[]>(`${this.baseUrl}api/TourCategory`);
        let that = this;
        data.subscribe((result) => {
            // console.log("Respone:"+result.body);
            let tourCategories = [];
            let tourCatePagings = [];
            result.body.forEach((d) => {
                tourCategories.push({
                    id: d.id,
                    name: d.name,
                    description: d.description,
                    image: d.image
                });
            });
            console.log("that" + tourCatePagings);
            while (tourCategories.length) {
                console.log("that" + tourCatePagings);
                tourCatePagings.push({tourCate: tourCategories.splice(0, 6), active: ""});
            }
            tourCatePagings[0].active = "active";
            that.tourCatePagings = tourCatePagings;
            console.log("that" + that.tourCatePagings.length);
        }, error => console.error(error));
    }

    private getBanners() {
        var data = this._dataService.getFull<Banner[]>(`${this.baseUrl}api/Banner`);
        let that = this;
        data.subscribe((result) => {
            // console.log("Respone:"+result.body);
            let banners = [];
            result.body.forEach((d, i) => {
                banners.push({
                    id: d.id,
                    name: d.name,
                    description: d.description,
                    image: d.image,
                    active: i == 0 ? "active" : ""
                });
            });
            that.banners = banners;
            console.log(that.banners);
        }, error => console.error(error));
    }
}
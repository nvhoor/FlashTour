  import {Component, Inject, OnInit, Renderer2} from '@angular/core';
import {DataService} from "@app/services";
import {DOCUMENT} from "@angular/common";
@Component({
  selector: 'appc-home-detail',
  templateUrl: './home-detail.component.html',
  styleUrls: ['./home-detail.component.scss']
})
export class HomeDetailComponent implements OnInit {
  public hotestTours: Hotest[];
  public newestTours: Newest[];
  public searchingTours: TourCard[];
  public searchingPosts: PostCate[];
  public emitSearch:EmitSearch;
  public isCountDown=true;
  public isCountDownSearch=true;
  public currentPage=0;
  public pageNums:PageNum[];
  public tourPagings:TourPagings[];
  public emitSearchPost:EmitSearchPost;
  public typePagingChosen={
    Pre:0,
    Num:1,
    Next:2
  };
  constructor(  @Inject("BASE_URL") private baseUrl: string,
                private _renderer2: Renderer2,
                private _dataService: DataService,
                @Inject(DOCUMENT) private _document: Document) { }
  ngOnInit() {
    this.getHostestTour();
    this.getNewsetTour();
    if(this.isCountDown){
      this.isCountDown=false;
      var timer=setTimeout(() => {
        this.appendScriptCountDown();
        this.isCountDown=true;
      }, 5000);
    }
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
  private appendScriptCountDownSearching() {
    console.log("appendScriptCountDownSearching lenght:" + this.searchingTours.length);
    this.searchingTours.forEach((tour) => {
      let script = this._renderer2.createElement('script');
      script.type = `text/javascript`;
      let id = tour.id.replace(/-/g, "");
      // console.log(id);
      script.text = `
            {
                 let countDownDatesearch${id} = new Date("${tour.departureDate}").getTime();
                  let xsearch${id} = setInterval(function() {
                    let nowsearch${id} = new Date().getTime();
                    let distancesearch${id} = countDownDatesearch${id} - nowsearch${id};
                    let dayssearch${id} = Math.floor(distancesearch${id} / (1000 * 60 * 60 * 24));
                    let hourssearch${id} = Math.floor((distancesearch${id} % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                    let minutessearch${id} = Math.floor((distancesearch${id} % (1000 * 60 * 60)) / (1000 * 60));
                    let secondssearch${id} = Math.floor((distancesearch${id} % (1000 * 60)) / 1000);
                    try{
                    document.getElementById("search_${tour.id}").innerHTML = dayssearch${id} + " days " + hourssearch${id} + "h "
                            + minutessearch${id} + "m " + secondssearch${id} + "s ";
                            }catch(e){
                            clearInterval(xsearch${id});
                            }
                    if (distancesearch${id} < 0) {
                      clearInterval(xsearch${id});
                      document.getElementById("search_${tour.id}").innerHTML = "EXPIRED";
                    }
                  }, 1000);
            }
        `;
      var parentEle=this._document.getElementById("parent_search_" + tour.id);
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
            endDatePro: d.tours[0].endDatePro,
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
            endDatePro: d.endDatePro,
          touristType: d.touristType,
        });
      });
      that.newestTours = newestTours;
      console.log(that.newestTours);
    }, error => console.error(error));
  }
  public getToursByOption(emit){
    this.emitSearch=emit;
    console.log("emit:",JSON.stringify(emit));
    var data = this._dataService.get<TourCard[]>(`${this.baseUrl}api/Tour/Search`,emit.option);
    let that = this;
    data.subscribe((result) => {
      // console.log("Respone:"+result.body);
      let searchingTours = [];
      let tourPagings=[];
      let pageNums=[];
      result.forEach((d) => {
        searchingTours.push({
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
            endDatePro: d.endDatePro,
          touristType: d.touristType,
        });
      });
      that.searchingTours = searchingTours;
      let i=0;
      while (searchingTours.length) {
        pageNums.push({num:i,active:""});
        tourPagings.push({tours: searchingTours.splice(0, 8), pageNum: i});
        i++
      }
      this.tourPagings = tourPagings;
      if(this.tourPagings.length>0){
        this.searchingTours=this.tourPagings[this.currentPage].tours;
        pageNums[0].active="active";
        this.pageNums= pageNums;
      }else{
        this.searchingTours=[];
        this.pageNums=[];
      }
      if(this.isCountDownSearch){
        this.isCountDownSearch=false;
        var timer=setTimeout(() => {
          this.appendScriptCountDownSearching();
          this.isCountDownSearch=true;
        }, 5000);
      }
      
      console.log(that.searchingTours);
    }, error => console.error(error));
  }
  changePage(typeChosen:number,num: number) {
    var curPape=this.currentPage;
    switch (typeChosen) {
      case this.typePagingChosen.Pre:
        if(this.currentPage-1>=0){
          this.currentPage--;
        }else{
          return;
        }
        break;
      case this.typePagingChosen.Num:
        this.currentPage=num;
        break;
      case this.typePagingChosen.Next:
        if(this.currentPage+1<this.tourPagings.length){
          this.currentPage++;
        }else{
          return;
        }
        break;
    }
    console.log("Chosen page:",this.currentPage);
    this.pageNums[curPape].active="";
    this.pageNums[this.currentPage].active="active";
    this.searchingTours=this.tourPagings[this.currentPage].tours;
    if(this.isCountDown){
      this.isCountDown=false;
      var timer=setTimeout(() => {
        this.appendScriptCountDown();
        this.isCountDown=true;
      }, 5000);
    }
  }
    checkExpiredPromotion(tour) {
        var toCheck = new Date().getTime();
        var startDatePro = new Date(tour.startDatePro).getTime();
        var endDatePro = new Date(tour.endDatePro).getTime();
        return toCheck >= startDatePro && toCheck <= endDatePro;
    }
    getPrice(tour) {
        if (this.checkExpiredPromotion(tour)) {
            return tour.promotionPrice;
        } else {
            return tour.originalPrice;
        }
    }
  public getPostsByOption(emitPost){
    this.emitSearchPost=emitPost;
    console.log("emit:",JSON.stringify(emitPost));
    var data = this._dataService.get<Post[]>(`${this.baseUrl}api/post/Search`);
    let that = this;
    data.subscribe((result) => {
      // console.log("Respone:"+result.body);
      let searchingPosts = [];
      let tourPagings=[];
      let pageNums=[];
      result.forEach((d) => {
        searchingPosts.push({
          id: d.id,
          name: d.name,
          image: d.image,
          description: d.description,
        });
      });
      console.log(that.searchingPosts);
    }, error => console.error(error));
  }
}

import {Component, Host, Inject, OnInit, Renderer2} from '@angular/core';
import {DataService} from "@app/services";
import {DOCUMENT} from "@angular/common";
import {HomeDetailComponent} from "@app/user/home-detail/home-detail.component";

@Component({
  selector: 'appc-tour-category',
  templateUrl: './tour-category.component.html',
  styleUrls: ['./tour-category.component.scss']
})
export class TourCategoryComponent implements OnInit {
  public tourCatePagings: TourCatePagings[];
 
  public tours:TourCard[];
  
  public name:string;
  public description:string;
  public type:string;
  public isCountDown=true;
  public currentPage=0;
  public pageNums:PageNum[];
  public tourPagings:TourPagings[];
  public typePagingChosen={
    Pre:0,
    Num:1,
    Next:2
  };
  constructor( @Inject("BASE_URL") private baseUrl: string,
               private _renderer2: Renderer2,
               private _dataService: DataService,
               @Inject(DOCUMENT) private _document: Document,
               @Host() private parent: HomeDetailComponent) { }

  ngOnInit() {
    this.getTourCategories();
  }
  private getTourCategories() {
    var data = this._dataService.getFull<TourCategory[]>(`${this.baseUrl}api/TourCategory`);
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
      while (tourCategories.length) {
        tourCatePagings.push({tourCate: tourCategories.splice(0, 6), active: ""});
      }
      tourCatePagings[0].active = "active";
      that.tourCatePagings = tourCatePagings;
    }, error => console.error(error));
  }
  private getTours(id,name,description) {
    this.name=name;
    this.description=description;
    this.currentPage=0;
    var url=`${this.baseUrl}api/Tour/toursByCateId/${id}`;
    var data = this._dataService.getFull<TourCard[]>(url);
    data.subscribe((result) => {
      // console.log("Respone:"+result.body);
      let tours = [];
      let tourPagings=[];
      let pageNums=[];
      result.body.forEach((d) => {
        tours.push({
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
      let i=0;
      while (tours.length) {
        pageNums.push({num:i,active:""});
        tourPagings.push({tours: tours.splice(0, 8), pageNum: i});
        i++
      }
      this.tourPagings = tourPagings;
      if(this.tourPagings.length>0){
        this.tours=this.tourPagings[this.currentPage].tours;
        pageNums[0].active="active";
        this.pageNums= pageNums; 
      }else{
        this.tours=[];
        this.pageNums=[];
      }
      if(this.isCountDown){
        this.isCountDown=false;
        var timer=setTimeout(() => {
          this.appendScriptCountDown();
          this.isCountDown=true;
        }, 5000);
      }
      console.log("Tour pagings:"+JSON.stringify(this.tourPagings),this.pageNums.length);
    }, error => console.error(error));
  }
  private appendScriptCountDown() {
    this.tours.forEach((tour) => {
      let script = this._renderer2.createElement('script');
      script.type = `text/javascript`;
      let id = tour.id.replace(/-/g, "");
      // console.log(id);
      script.text = `
            {
                 let countDownDatecate${id} = new Date("${tour.departureDate}").getTime();
                  let xcate${id} = setInterval(function() {
                    let nowcate${id} = new Date().getTime();
                    let distancecate${id} = countDownDatecate${id} - nowcate${id};
                    let dayscate${id} = Math.floor(distancecate${id} / (1000 * 60 * 60 * 24));
                    let hourscate${id} = Math.floor((distancecate${id} % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                    let minutescate${id} = Math.floor((distancecate${id} % (1000 * 60 * 60)) / (1000 * 60));
                    let secondscate${id} = Math.floor((distancecate${id} % (1000 * 60)) / 1000);
                    try{
                    document.getElementById("cate_${tour.id}").innerHTML = dayscate${id} + " days " + hourscate${id} + "h "
                            + minutescate${id} + "m " + secondscate${id} + "s ";
                            }catch(e){
                            clearInterval(xcate${id});
                            }
                    if (distancecate${id} < 0) {
                      clearInterval(xcate${id});
                      document.getElementById("cate_${tour.id}").innerHTML = "EXPIRED";
                    }
                  }, 1000);
            }
        `;
      var parentEle=this._document.getElementById("parent_cate_" + tour.id);
      parentEle&&this._renderer2.appendChild(parentEle, script);
    });
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
    this.tours=this.tourPagings[this.currentPage].tours;
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
}

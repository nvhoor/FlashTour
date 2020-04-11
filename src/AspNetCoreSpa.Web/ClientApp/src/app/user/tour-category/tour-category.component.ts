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
  public pageNums:number[];
  public tours:TourCard[];
  public tourPagings:TourPagings[];
  public name:string;
  public description:string;
  public type:string;
  public currentPage=0;
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
          touristType: d.touristType,
        });
      });
      let i=0;
      while (tours.length) {
        pageNums.push(i);
        tourPagings.push({tours: tours.splice(0, 8), pageNum: i});
        i++
      }
      this.tourPagings = tourPagings;
      this.tours=this.tourPagings[this.currentPage].tours;
      this.pageNums= pageNums;
      console.log("Tour pagings:"+JSON.stringify(this.tourPagings),this.pageNums.length);
    }, error => console.error(error));
  }

  changePage(typeChosen:number,num: number) {
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
    this.tours=this.tourPagings[this.currentPage].tours;
  }
}

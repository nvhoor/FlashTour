import {Component, EventEmitter, Inject, Input, OnInit, Output, Renderer2} from '@angular/core';
import {DataService} from "@app/services";
import {DOCUMENT} from "@angular/common";
import {ActivatedRoute} from "@angular/router";
import {FormsService} from "@app/shared";

@Component({
  selector: 'appc-banner',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.scss']
})
export class BannerComponent implements OnInit {
  public banners: Banner[];
  public provinces:Province[];
  public tourCategories:TourCategory[];
  public postCategories: PostCate[];
  public prices:SearchPrice[];
  public departureDate:Date;
  public selectedDeparture:string;
  public selectedCatePost:string;
  @Input() optionSearch:OptionSearch;
  @Input() optionSearchPost:OptionSearchPost;
  public emitSearch:EmitSearch;
  public emitSearchPost:EmitSearchPost;
  constructor(       @Inject("BASE_URL") public baseUrl: string,
                     private route: ActivatedRoute,
                     private _renderer2: Renderer2,
                     private _dataService:DataService,
                     private formsService: FormsService,
                     @Inject(DOCUMENT) private _document: Document) { 
    this.prices=[
      {id:0,value:"--All--"},
      {id:1,value:"< 100 Dollar"},
      {id:2,value:"100-300 Dollar"},
      {id:3,value:"300-600 Dollar"},
      {id:4,value:"600-800 Dollar"},
      {id:5,value:"800-1000 Dollar"},
      {id:6,value:"> 1000 Dollar"}
    ];
    this.departureDate=new Date();
    this.optionSearch={
      departureId:"0",
      destinationId:"0",
      departureDateTimeStamp:this.departureDate.getTime()+'',
      tourCategoryId:"0",
      priceId:0,
    }
    this.optionSearchPost = {
      postCategoryId:"0"

    }
  }

  ngOnInit() {
    this.getBanners();
    this.getProvinces();
    this.getTourCategories();
    this.getPostCategories();
  }
  @Output() myEvent = new EventEmitter<EmitSearch>();
  onClickSearchTour(){
      var departureSelect=this._document.getElementById("Departureid");
      if(this.optionSearch.departureId=='0'){
          alert("You need to choose a departure place!");
          departureSelect.classList.add("btn-outline-danger");
          departureSelect.focus();
          return;
      }
      departureSelect.classList.remove("btn-outline-danger");
      this.optionSearch.departureDateTimeStamp=this.departureDate.getTime()+'';
      this.selectedDeparture=this.provinces.find(x=>x.id==this.optionSearch.departureId).name;
      this.emitSearch={departureName:this.selectedDeparture,destinationName:"",option:this.optionSearch};
    this.myEvent.emit(this.emitSearch);
  }

  @Output() myEventPost = new EventEmitter<EmitSearchPost>();
  onClickSearchPost(){
    var selectedDeparture=this._document.getElementById("Departureid");
    if(this.optionSearchPost.postCategoryId=='0'){
      alert("You need to choose a Post Category!");
      selectedDeparture.classList.add("btn-outline-danger");
      selectedDeparture.focus();
      return;
    }
    this.selectedCatePost=this.postCategories.find(x=>x.id==this.optionSearchPost.postCategoryId).name;
    this.emitSearchPost={categoryName:this.selectedCatePost,option:this.optionSearchPost};
    this.myEventPost.emit(this.emitSearchPost);
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
  private getProvinces() {
    var data = this._dataService.getFull<Province[]>(`${this.baseUrl}api/Province`);
    let that = this;
    data.subscribe((result) => {
      // console.log("Respone:"+result.body);
      let provinces = [];
      result.body.forEach((d, i) => {
        provinces.push({
          id: d.id,
          name: d.name
        });
      });
      that.provinces = provinces;
      this.selectedDeparture=that.provinces[0].name;
      console.log(that.provinces);
    }, error => console.error(error));
  }
  private getTourCategories() {
    var data = this._dataService.getFull<TourCategory[]>(`${this.baseUrl}api/TourCategory`);
    let that = this;
    data.subscribe((result) => {
      // console.log("Respone:"+result.body);
      let tourCategories = [];
      result.body.forEach((d, i) => {
        tourCategories.push({
          id: d.id,
          name: d.name
        });
      });
      that.tourCategories = tourCategories;
      console.log(that.tourCategories);
    }, error => console.error(error));
  }
    public convertDate(dateInput) {
      console.log(dateInput);
        function pad(s) { return (s < 10) ? '0' + s : s; }
        var date = new Date(dateInput)||new Date();
        return [pad(date.getDate()), pad(date.getMonth()+1), date.getFullYear()].join('/')
    }
  private getPostCategories() {
    var data = this._dataService.getFull<PostCate[]>(`${this.baseUrl}api/postcategory`);
    let that = this;
    data.subscribe((result) => {
      // console.log("Respone:"+result.body);
      let postCategories = [];
      result.body.forEach((d, i) => {
        postCategories.push({
          id: d.id,
          name: d.name
        });
      });
      that.postCategories = postCategories;
      console.log(that.postCategories);
    }, error => console.error(error));
  }
}

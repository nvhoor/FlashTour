import {Component, Inject, OnInit, Renderer2} from '@angular/core';
import {DataService} from "@app/services";
import {DOCUMENT} from "@angular/common";

@Component({
  selector: 'appc-banner',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.scss']
})
export class BannerComponent implements OnInit {
  public banners: Banner[];
  constructor(  @Inject("BASE_URL") private baseUrl: string,
                private _renderer2: Renderer2,
                private _dataService: DataService,
                @Inject(DOCUMENT) private _document: Document,) { }

  ngOnInit() {
   
    this.getBanners();

   
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

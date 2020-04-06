import {Component, Inject, OnInit, Renderer2} from '@angular/core';
import {DOCUMENT} from "@angular/common";
import {DataService} from "@app/services";
import {ActivatedRoute} from '@angular/router';

@Component({
    selector: 'appc-user-detail-component',
    templateUrl: './detail.component.html',
    styleUrls:['./detail.component.scss']
})
export class DetailComponent implements OnInit{
    public tour:Tour;
    public toursByCategory:TourCard[];
    public tourImages:CarouselImage[];
    constructor(
        @Inject("BASE_URL") private baseUrl: string,
        private route: ActivatedRoute,
        private _renderer2: Renderer2,
        private _dataService:DataService,
        @Inject(DOCUMENT) private _document: Document,
    ) {
    }
    public ngOnInit() {
        var id=this.route.snapshot.params['id'];
        this.getTourById(id);
        this.getToursSameCategoryById(id);
        setTimeout( () =>{
            this.appendScriptCountDown();
        },5000);
        console.log("ngOnInit");
            this._dataService.put<Tour>(`${this.baseUrl}api/Tour/IncreaseViewCount/${id}`,this.tour).subscribe(x=>{
                console.log("increase view count success!");
            },error => {  console.error(error);});
        
    }
    private appendScriptCountDown() {
        console.log("lenght:" + this.toursByCategory.length);
        this.toursByCategory.forEach((tour) => {
            let script = this._renderer2.createElement('script');
            script.type = `text/javascript`;
            let id = tour.id.replace(/-/g, "");
            // console.log(id);
            script.text = `
            {
                 let countDownDateSimilar${id} = new Date("${tour.departureDate}").getTime();
                  let xSimilar${id} = setInterval(function() {
                    let nowSimilar${id} = new Date().getTime();
                    let distanceSimilar${id} = countDownDateSimilar${id} - nowSimilar${id};
                    let daysSimilar${id} = Math.floor(distanceSimilar${id} / (1000 * 60 * 60 * 24));
                    let hoursSimilar${id} = Math.floor((distanceSimilar${id} % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                    let minutesSimilar${id} = Math.floor((distanceSimilar${id} % (1000 * 60 * 60)) / (1000 * 60));
                    let secondsSimilar${id} = Math.floor((distanceSimilar${id} % (1000 * 60)) / 1000);
                    try{
                    document.getElementById("similar_${tour.id}").innerHTML = daysSimilar${id} + " days " + hoursSimilar${id} + "h "
                            + minutesSimilar${id} + "m " + secondsSimilar${id} + "s ";
                            }catch(e){
                            clearInterval(xSimilar${id});
                            }
                    if (distanceSimilar${id} < 0) {
                      clearInterval(xSimilar${id});
                      document.getElementById("similar_${tour.id}").innerHTML = "EXPIRED";
                    }
                  }, 1000);
            }
        `;
            var parentEle=this._document.getElementById("parent_similar_" + tour.id);
            parentEle&&this._renderer2.appendChild(parentEle, script);
        });
    }
   
    private getToursSameCategoryById(id) {
        var data = this._dataService.getFull<TourCard[]>(`${this.baseUrl}api/Tour/toursByCate/${id}`);
        let that = this;
        data.subscribe((result) => {
            // console.log("Respone:"+result.body);
            let toursByCategory = [];
            result.body.forEach((d, i) => {
                toursByCategory.push({
                    id:d.id,
                    name:d.name,
                    image:d.image,
                    description:d.description,
                    departureDate:d.departureDate,
                    departureId:d.departureId,
                    slot:d.slot,
                    originalPrice:d.originalPrice,
                    promotionPrice:d.promotionPrice,
                    startDatePro:d.startDatePro,
                    touristType:d.touristType
                });
            });
            that.toursByCategory = toursByCategory;
            console.log(that.toursByCategory);
        }, error => console.error(error));
    }

    private getTourById(id) {
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
            var images=result.images.split("|");
            var carouselImages=[];
            images.forEach((image,index)=>{
                carouselImages.push({index:index,name:image,active:""});
            });
            that.tourImages=carouselImages;
            this.tourImages[0].active="active";
        }, error => console.error(error));
    }
}
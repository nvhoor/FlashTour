import {Component, HostBinding, Input, OnInit} from '@angular/core';
import {FieldTypes, IAppTableOptions} from "@app/models";
import {Validators} from "@angular/forms";
import {IProduct} from "@app/+examples/examples/crud-shop/crud-shop.models";
import {ChartDataSets, ChartOptions} from "chart.js";
import {Color, Label} from "ng2-charts";
import {DataService} from "@app/services";

@Component({
  selector: 'appc-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  @HostBinding('class')
  elementClass = 'col-lg-10 col-md-9 bg-light content';
  public startDay="";
  public endDay=this.formatDate(Date.now());
  public lastStartDay="";
  public lastEndDay="";
  public lineChartData: ChartDataSets[] = [
    { data: [], label: 'Revenue per day' }
  ];
    public lineChartTouristData: ChartDataSets[] = [
        { data: [], label: 'Tourists per day' }
    ];
  public lineChartLabels: Label[] = [];
  public lineChartOptions: (ChartOptions & { annotation: any }) = {
    annotation: undefined,
    responsive: true
  };
  public lineChartColors: Color[] = [
    {
      borderColor: 'green',
      backgroundColor: 'rgba(179,255,0,0.3)',
    },
  ];
    public lineChartTouristColors: Color[] = [
        {
            borderColor: 'orange',
            backgroundColor: 'rgba(255,119,0,0.3)',
        },
    ];
  public lineChartLegend = true;
  public lineChartType = 'line';
  public lineChartPlugins = [];
  constructor(private _dataService:DataService,) {
    var date=new Date();
    this.startDay=this.formatDate(date.setDate(date.getDate()-7));
      this.lastStartDay=this.startDay;
      this.lastEndDay=this.endDay;
  }

  ngOnInit() {
    this._dataService.get<any[]>(`api/TourBooking/GetDashBoardData`,{startDateTimeStamp:new Date(this.startDay).getTime(),endDateTimeStamp:new Date(this.endDay).getTime()})
        .subscribe(datas => {
          console.log("datas",JSON.stringify(datas));
            var data=[];
            var tourists=[];
            var label=[];
            var that=this;
            // @ts-ignore
            datas.revenues.forEach(x=>{
                data.push(x.y);
                label.push(that.formatDate(new Date(x.x)));
            });
            // @ts-ignore
            datas.tourists.forEach(x=>{
                tourists.push(x.y);
            });
            this.lineChartData[0].data=data;
            this.lineChartTouristData[0].data=tourists;
            this.lineChartLabels=label;
        });
  }

  changeDateRange() {
    var startDate=new Date(this.startDay);
    var endDate= new Date(this.endDay);
  
    // if(endDate.getTime()>Date.now()){
    //   alert("End date must less than or equal current date!");
    //   return;
    // }
    if(Math.round((endDate.getTime()-startDate.getTime())/(1000*60*60*24))>=10){
      alert("Max range between start date and end date are 10!");
      this.startDay=this.lastStartDay;
      this.endDay=this.lastEndDay;
      return;
    }
      this.lastStartDay=this.startDay;
      this.lastEndDay=this.endDay;
    this._dataService.get<any[]>(`api/TourBooking/GetDashBoardData`,{startDateTimeStamp:startDate.getTime(),endDateTimeStamp:endDate.getTime()})
        .subscribe(datas => {
          console.log("datas",JSON.stringify(datas));
          var data=[];
          var tourists=[];
          var label=[];
          var that=this;
          // @ts-ignore
            datas.revenues.forEach(x=>{
            data.push(x.y);
            label.push(that.formatDate(new Date(x.x)));
          });
            // @ts-ignore
            datas.tourists.forEach(x=>{
                tourists.push(x.y);
            });
          this.lineChartData[0].data=data;
            this.lineChartTouristData[0].data=tourists;
          this.lineChartLabels=label;
        });
  }
   formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
      month = '0' + month;
    if (day.length < 2)
      day = '0' + day;

    return [year, month, day].join('-');
  }
}

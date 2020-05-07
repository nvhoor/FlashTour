import {Component, HostBinding, Inject, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {FieldTypes, IAppTableOptions, IOption} from "@app/models";
import {AppTableComponent} from "@app/shared";
import {DataService, ModalService} from "@app/services";
import {ToastrService} from "@app/toastr";
import {Validators} from "@angular/forms";

@Component({
  selector: 'appc-manage-tour-programs-staff',
  templateUrl: './manage-tour-programs-staff.component.html',
  styleUrls: ['./manage-tour-programs-staff.component.scss']
})
export class ManageTourProgramsStaffComponent implements OnInit {

  @HostBinding('class')
  elementClass = 'col-lg-10 col-md-9 bg-light content';
  options: IAppTableOptions<Comunication>;
  tourFieldOption:IOption[];
  @ViewChild('tourTemplate', { static: true }) tourTemplate: TemplateRef<any>;
  @ViewChild('table', { static: true }) table:AppTableComponent;
  chosenEdit= true;
  constructor(
      @Inject("BASE_URL") private baseUrl: string,
      private modalService: ModalService,
      private _dataService:DataService,
      private toastr: ToastrService,) { }
  ngOnInit() {
    this.options={apiUrl:'api/tourprogram'};
    var data = this._dataService.getFull<PostCate>(`${this.baseUrl}api/tour`);
    let that = this;
    data.subscribe((result) => {
      console.log("Tour Name: ",JSON.stringify(result.body));
      var fieldOptions=[];
      result.body.forEach((d, i) => {
        fieldOptions.push({
          key:d.id,
          value:d.name
        });
      });
      this.tourFieldOption=fieldOptions;
      this.newPost();
    });
  }
  newPost(){
    this.options = {
      title: 'Tour programs',
      apiUrl: 'api/tourprogram',
      disableFilter: true,
      disableFilterr: false,
      disablechangetour: true,
      disableviewContact: true,
      enablepostCensorship: false,
      columns: [
        { prop: 'date', name: 'Date', fieldType: FieldTypes.Date, fieldValidations: [Validators.required] },
        { prop: 'orderNumber', name: 'OrderNumber', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
        { prop: 'title', name: 'Title', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
        { prop: 'description', name: 'Description', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'destination', name: 'Destination', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'tourId', name: 'Tour Name', fieldType: FieldTypes.Select,
          fieldOptions: this.tourFieldOption,cellTemplate: this.tourTemplate},
      ]};
    this.table.updateData('api/tourprogram');
  }
}

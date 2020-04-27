import {Component, HostBinding, Inject, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {FieldTypes, IAppTableOptions, IFieldConfig} from "@app/models";
import {Validators} from "@angular/forms";
import {clone} from 'lodash';
import {AppFormComponent, AppTableComponent} from "@app/shared";
import {DataService, ModalService} from "@app/services";
import {ToastrService} from "@app/toastr";

@Component({
  selector: 'appc-manage-tours',
  templateUrl: './manage-tours.component.html',
  styleUrls: ['./manage-tours.component.scss']
})
export class ManageToursComponent implements OnInit {
  @HostBinding('class')
  elementClass = 'col-lg-10 col-md-9 bg-light content';
  options: IAppTableOptions<Comunication>;
  chosenEdit=true;
  @ViewChild('tourPricesTemplate', { static: true }) tourPricesTemplate: TemplateRef<any>;
  @ViewChild('sensorshipTourTemplate', { static: true }) sensorshipTourTemplate: TemplateRef<any>;
  @ViewChild('formTemplate', { static: true }) formTemplate: AppFormComponent;
  @ViewChild('table', { static: true }) table:AppTableComponent;
  constructor(
      @Inject("BASE_URL") private baseUrl: string,
      private modalService: ModalService,
      private _dataService:DataService,
      private toastr: ToastrService,
  ) { }

  ngOnInit() {
    this.newTour();
  }
  private onClickTourPrices(columns,row) {
    var model = row.prices[0];
    const fields = columns
        .filter(f => f.fieldType)
        .map(x => {
          var onSubmit;
          if(x.fieldType==FieldTypes.Button){
            onSubmit=x.onSubmit;
          }else{
            onSubmit=null;
          }
          const field: IFieldConfig = {
            name: x.prop.toString(),
            type: x.fieldType,
            label: x.name,
            validation: x.fieldValidations,
            options: x.fieldOptions,
            onSubmit:onSubmit
          };
          return field;
        });

    fields.push({
      name: 'buttonCreatePrice',
      type: FieldTypes.Button,
      label: 'Create',
    });
    fields.push({
      name: 'buttonUpdatePrice',
      type: FieldTypes.Button,
      label: 'Update',
    });
    const template = clone(<any>this.formTemplate);
    template.data = { formConfig: fields, formModel: (model || {}),pricesData: row.prices,parenTable:this.table};
    return this.modalService.confirm({
      title:'Tour Prices',
      template,
    });
  }

  newTour(){
    this.options = {
      title: 'Tours',
      id:'tour',
      apiUrl: 'api/tour/',
      disableDelete:true,
      //changetour: true,
      enabletourCensorship: false,
      disableFilter: false,
      //changetour: true,
      columns: [
        { prop: 'name', name: 'Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'image', name: 'Image', fieldType: FieldTypes.Textbox },
        { prop: 'images', name: 'Images', fieldType: FieldTypes.Textbox },
        { prop: 'description', name: 'Description', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
        { prop: 'departureDate', name: 'DepartureDate', fieldType: FieldTypes.Date, fieldValidations: [Validators.required] },
        //{ prop: 'departureId', name: 'departureName', fieldType: FieldTypes.Textbox, },
        { prop: 'slot', name: 'Slot', fieldType: FieldTypes.Number, fieldValidations: [Validators.required] },
        { prop: 'tourCategoryId', name: 'Tour cateogry', fieldType: FieldTypes.Select,
          fieldOptions:[
            {key:"67bb3560-7621-42fc-bf20-bdf183cd222e",value:'Tour Hà Nội'},
            {key:"67bb3560-7621-42fc-bf20-bdf183cd222e",value:'Tour Hồ Chí Minh'},
          ]},
        { prop: 'prices', name: 'Tour Prices',cellTemplate:this.tourPricesTemplate,
          subTableColumn:[
            { prop: 'tourId', name: 'Tour ID', fieldType: FieldTypes.Textbox,  },
            { prop: 'name', name: 'Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required]  },
            { prop: 'originalPrice', name: 'Original Price', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required]},
            { prop: 'promotionPrice', name: 'promotionPrice', fieldType: FieldTypes.Textbox,},
            { prop: 'startDatePro', name: 'Start Date Promotion', fieldType: FieldTypes.Date, fieldValidations: [Validators.required] },
            { prop: 'endDatePro', name: 'End Date Promotion', fieldType: FieldTypes.Date, fieldValidations: [Validators.required] },
            { prop: 'touristType', name: 'Tourist Type', fieldValidations: [Validators.required], fieldType: FieldTypes.Select,
              fieldOptions:[
                {key:0,value:'Adult'},
                {key:1,value:'Children'},
                {key:2,value:'Kid'},
              ]},
          ]
        }
      ]
    };
    this.chosenEdit=true;
    this.table.updateData('api/tour');
  }
  clickCensorship(){
    this.options = {
      title: 'Cencership Tours',
      id:'tour',
      apiUrl: 'api/tour/cencershiptour',
      disableEditing:true,
      enabletourCensorship:true,
      columns: [
        { prop: 'name', name: 'Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'image', name: 'Image', fieldType: FieldTypes.Textbox },
        { prop: 'images', name: 'Images', fieldType: FieldTypes.Textbox },
        { prop: 'description', name: 'Description', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
        { prop: 'departureDate', name: 'DepartureDate', fieldType: FieldTypes.Date, fieldValidations: [Validators.required] },
        //{ prop: 'departureId', name: 'departureName', fieldType: FieldTypes.Textbox, },
        { prop: 'slot', name: 'Slot', fieldType: FieldTypes.Number, fieldValidations: [Validators.required] },
        { prop: 'tourCategoryId', name: 'Tour cateogry', fieldType: FieldTypes.Select,
          fieldOptions:[
            {key:"67bb3560-7621-42fc-bf20-bdf183cd222e",value:'Tour Hà Nội'},
            {key:"67bb3560-7621-42fc-bf20-bdf183cd222e",value:'Tour Hồ Chí Minh'},
          ]},
        { prop: 'prices', name: 'Tour Prices',cellTemplate:this.tourPricesTemplate,
          subTableColumn:[
            { prop: 'tourId', name: 'Tour ID', fieldType: FieldTypes.Textbox,  },
            { prop: 'name', name: 'Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required]  },
            { prop: 'originalPrice', name: 'Original Price', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required]},
            { prop: 'promotionPrice', name: 'promotionPrice', fieldType: FieldTypes.Textbox,},
            { prop: 'startDatePro', name: 'Start Date Promotion', fieldType: FieldTypes.Date, fieldValidations: [Validators.required] },
            { prop: 'endDatePro', name: 'End Date Promotion', fieldType: FieldTypes.Date, fieldValidations: [Validators.required] },
            { prop: 'touristType', name: 'Tourist Type', fieldValidations: [Validators.required], fieldType: FieldTypes.Select,
              fieldOptions:[
                {key:0,value:'Adult'},
                {key:1,value:'Children'},
                {key:2,value:'Kid'},
              ]},
          ]
        }
      ]
    };
    this.chosenEdit=false;
    this.table.updateData('api/tour/cencershiptour');
  }
  /*
      clicktourCustomer(){
          this.options = {
              title: 'Tour Customer',
              id:'tour',
              apiUrl: 'api/tour/tourcustomer',
              disableEditing:true,
              enabletourCensorship:true,
              columns: [
                  { prop: 'FullName', name: 'FullName', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
                  { prop: 'Gender', name: 'Gender',
                      fieldOptions:[
                          {key:0,value:'Male'},
                          {key:1,value:'Female'},
                      ]},
                  { prop: 'BirthDay', name: 'BirthDay', fieldType: FieldTypes.FileUpload },
                  { prop: 'TouristType', name: 'TouristType',
                      fieldOptions:[
                          {key:0,value:'Adult'},
                          {key:1,value:'Children'},
                          {key:2,value:'Kid'},
                      ]},
              ]
          }
          };
          this.chosenEdit=false;
          this.table.updateData('api/tour/tourcustomer');
      }*/
}

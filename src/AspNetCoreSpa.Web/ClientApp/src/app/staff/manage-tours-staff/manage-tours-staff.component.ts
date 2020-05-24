import {Component, HostBinding, Inject, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {FieldTypes, IAppTableOptions, IFieldConfig, IOption} from "@app/models";
import {clone} from 'lodash';
import {Validators} from "@angular/forms";
import {AppFormComponent, AppTableComponent, FormsService} from "@app/shared";
import {DataService, ModalService} from "@app/services";
import {ToastrService} from "@app/toastr";

@Component({
  selector: 'appc-manage-tours-staff',
  templateUrl: './manage-tours-staff.component.html',
  styleUrls: ['./manage-tours-staff.component.scss']
})
export class ManageToursStaffComponent implements OnInit {
  @HostBinding('class')
  elementClass = 'col-lg-10 col-md-9 bg-light content';
  options: IAppTableOptions<Tour>;
  chosenEdit=true;
  @ViewChild('tourPricesTemplate', { static: true }) tourPricesTemplate: TemplateRef<any>;
  @ViewChild('tourCategoriesTemplate', { static: true }) tourCategoriesTemplate: TemplateRef<any>;
  @ViewChild('tourProgramsTemplate', { static: true }) tourProgramsTemplate: TemplateRef<any>;
  @ViewChild('formTemplate', { static: true }) formTemplate: AppFormComponent;
  @ViewChild('table', { static: true }) table:AppTableComponent;
  @ViewChild('departureTemplate', { static: true }) departureTemplate: TemplateRef<any>;
  @ViewChild('imagesTemplate', { static: true }) imagesTemplate: TemplateRef<any>;

  departuresFieldOption:IOption[];
  private tourcategoryFieldOption: IOption[];
  constructor(
      @Inject("BASE_URL") public baseUrl: string,
      private modalService: ModalService,
      private _dataService:DataService,
      private toastr: ToastrService,
      private formsService: FormsService,

  ) { }
  ngOnInit() {
    this.options={apiUrl:'api/tour/'};
    var data = this._dataService.getFull<Province[]>(`${this.baseUrl}api/Province`);
    let that = this;
    data.subscribe((result) => {
      console.log("All Province: ",JSON.stringify(result.body));
      var fieldOptions=[];
      result.body.forEach((d, i) => {
        fieldOptions.push({
          key:d.id,
          value:d.name
        });
      });
      this.departuresFieldOption=fieldOptions;
    });
    var datatourcate = this._dataService.getFull<Tourcate[]>(`${this.baseUrl}api/tourcategory`);
    let thattourcate = this;
    datatourcate.subscribe((result) => {
      console.log("All tourcategory: ",JSON.stringify(result.body));
      var fieldOptions=[];
      result.body.forEach((d, i) => {
        fieldOptions.push({
          key:d.id,
          value:d.name
        });
      });
      this.tourcategoryFieldOption=fieldOptions;
      this.newTour();
    });
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
    console.log("price",row.prices);
    const template = clone(<any>this.formTemplate);
    template.data = { formConfig: fields, formModel: (model || {}),pricesData: row.prices,parenTable:this.table};
    return this.modalService.confirm({
      title:'Tour Prices',
      template,
    });
  }
  private onClickImages(columns,row) {
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
      name: 'buttonAddImage',
      type: FieldTypes.Button,
      label: 'Add',
    });
    console.log("images",row.images);
    const template = clone(<any>this.formTemplate);
    template.data = { formConfig: fields, formModel: (model || {}),imagesData:this.getListImagesFromString(row),tourIdChosen:row.id ,parenTable:this.table};
    return this.modalService.confirm({
      title:'Tour Images',
      template,
    });
  }
  getListImagesFromString(tour){
    var images= tour.images&&tour.images.split("|")||[];
    if(images.length>0){
      return images.map((value)=> {
        return {
          tourId:tour.id,
          image:value
        }
      });
    }else{
      return [];
    }

  }
  private onClickTourPrograms(columns,row) {
    var model=row.tourPrograms[0];
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
      name: 'buttonCreateProgram',
      type: FieldTypes.Button,
      label: 'Create',
    });
    fields.push({
      name: 'buttonUpdateProgram',
      type: FieldTypes.Button,
      label: 'Update',
    });
    console.log("tourPrograms",row.tourPrograms);
    const template = clone(<any>this.formTemplate);
    template.data = { formConfig: fields, formModel: (model || {}),tourproData: row.tourPrograms,parenTable:this.table};

    return this.modalService.confirm({
      title:'Tour Program',
      template,
    });
  }
  newTour(){
    this.options = {
      title: 'Tours',
      id:'tour',
      apiUrl: 'api/Tour',
      disableDelete:true,
      enabletourCensorship: false,
      disableFilter: true,
      FilterName: true,
      disableviewContact: true,
      disableView:true,
      //changetour: true,
      columns: [
        { prop: 'id', name: 'ID Tour', fieldType: FieldTypes.Textbox },
        { prop: 'name', name: 'Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required,this.formsService.nameValidator] },
        { prop: 'image', name: 'Image', fieldType: FieldTypes.FileUpload ,imgSrcUrl:'api/Tour/UploadImage',fieldValidations: [Validators.required]},
        { prop: 'images', name: 'Images',cellTemplate:this.imagesTemplate,
          subTableColumn:[
            { prop: 'image', name: 'Image', fieldType: FieldTypes.FileUpload,imgSrcUrl:'api/Tour/UploadImage'}
          ]},
        { prop: 'description', name: 'Description', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
        { prop: 'departureDate', name: 'Departure Date', fieldType: FieldTypes.Date, fieldValidations: [Validators.required] },
        { prop: 'departureId', name: 'Departure Name', fieldType: FieldTypes.Select,fieldValidations: [Validators.required],  fieldOptions: this.departuresFieldOption,
          cellTemplate: this.departureTemplate},
        { prop: 'destinationId', name: 'Destinaion Name', fieldType: FieldTypes.Select,fieldValidations: [Validators.required],  fieldOptions: this.departuresFieldOption,
          cellTemplate: this.departureTemplate},
        { prop: 'slot', name: 'Slot', fieldType: FieldTypes.Number, fieldValidations: [Validators.required, this.formsService.numberNotZeroValidator] },
        { prop: 'tourCategoryId', name: 'Tour category', fieldType: FieldTypes.Select, fieldValidations: [Validators.required],
          fieldOptions: this.tourcategoryFieldOption,cellTemplate: this.tourCategoriesTemplate},
        // prices
        { prop: 'prices', name: 'Tour Prices',cellTemplate:this.tourPricesTemplate,
          subTableColumn:[
            { prop: 'tourId', name: 'Tour ID',fieldType: FieldTypes.Textbox,  },
            { prop: 'name', name: 'Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required, this.formsService.nameValidator]  },
            { prop: 'originalPrice', name: 'Original Price', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required, this.formsService.numberValidator]},
            { prop: 'promotionPrice', name: 'PromotionPrice', fieldType: FieldTypes.Textbox,},
            { prop: 'startDatePro', name: 'Start Date Promotion', fieldType: FieldTypes.Date,},
            { prop: 'endDatePro', name: 'End Date Promotion', fieldType: FieldTypes.Date, },
            { prop: 'touristType', name: 'Tourist Type', fieldValidations: [Validators.required], fieldType: FieldTypes.Select,
              fieldOptions:[
                {key:0,value:'Adult'},
                {key:1,value:'Children'},
                {key:2,value:'Kid'},
              ]},
          ]
        },
        //tourPrograms
        { prop: 'tourPrograms', name: 'Tour Program',cellTemplate:this.tourProgramsTemplate,
          subTableColumn:[
            { prop: 'tourId', name: 'Tour ID',fieldType: FieldTypes.Textbox,},
            { prop: 'date', name: 'Date', fieldType: FieldTypes.Date, fieldValidations: [Validators.required]  },
            { prop: 'orderNumber', name: 'Order Number', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required]},
            { prop: 'title', name: 'Title', fieldType: FieldTypes.Textbox,},
            { prop: 'description', name: 'Description', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
            { prop: 'destination', name: 'Destination', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
          ]
        }
      ]
    };
    this.chosenEdit=true;
    this.table.updateData('api/Tour');
  }

}

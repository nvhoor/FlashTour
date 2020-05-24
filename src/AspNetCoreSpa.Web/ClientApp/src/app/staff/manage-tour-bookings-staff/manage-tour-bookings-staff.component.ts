import {Component, HostBinding, Inject, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {FieldTypes, IAppTableOptions, IFieldConfig, IOption} from "@app/models";
import {Validators} from "@angular/forms";
import {clone} from 'lodash';
import {AppFormComponent, AppTableComponent,FormsService} from "@app/shared";
import {DataService, ModalService} from "@app/services";
import {ToastrService} from "@app/toastr";
@Component({
  selector: 'appc-manage-tour-bookings-staff',
  templateUrl: './manage-tour-bookings-staff.component.html',
  styleUrls: ['./manage-tour-bookings-staff.component.scss']
})
export class ManageTourBookingsStaffComponent implements OnInit {
  @HostBinding('class')
  elementClass = 'col-lg-10 col-md-9 bg-light content';
  options: IAppTableOptions<Comunication>;
  chosenEdit=true;
  @ViewChild('bookingPricesTemplate', { static: true }) bookingPricesTemplate: TemplateRef<any>;
  @ViewChild('customersTemplate', { static: true }) customersTemplate: TemplateRef<any>;
  @ViewChild('sensorshipTemplate', { static: true }) sensorshipTemplate: TemplateRef<any>;
  @ViewChild('formTemplate', { static: true }) formTemplate: AppFormComponent;
  @ViewChild('table', { static: true }) table:AppTableComponent;
  constructor(
      @Inject("BASE_URL") public baseUrl: string,
      private modalService: ModalService,
      private _dataService:DataService,
      private toastr: ToastrService,
      private formsService: FormsService,
  ) { }
  ngOnInit() {
    this.clickEditTour();
  }

  private onClickTourCustomers(columns,row) {
    var model=row.tourCustomers[0];
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
      name: 'buttonCreateCustomer',
      type: FieldTypes.Button,
      label: 'Create',
    });
    fields.push({
      name: 'buttonUpdateCustomer',
      type: FieldTypes.Button,
      label: 'Update',
    });
    const template = clone(<any>this.formTemplate);
    template.data = { formConfig: fields, formModel: (model || {}),customerData: row.tourCustomers,parenTable:this.table};

    return this.modalService.confirm({
      title:'Tour Customers',
      template,
    });
  }
  private onClickBookingPrices(columns,row) {
    var model=row.bookingPrices[0];
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
    const template = clone(<any>this.formTemplate);
    template.data = { formConfig: fields, formModel: (model || {}),priceData: row.bookingPrices,parenTable:this.table};

    return this.modalService.confirm({
      title:'Booking Prices',
      template,
    });
  }

  clickEditTour() {
    this.options = {
      id:'tour-booking',
      title: 'Edit tour bookings',
      apiUrl: 'api/TourBooking',
      disableDelete:true,
      disablechangetour: true,
      disableviewContact: true,
      disableFilterDepartue: true,
      disableFilterName: true,
      columns: [
        { prop: 'id', name: 'Id'},
        { prop: 'tourId', name: 'Tour Id', fieldType: FieldTypes.Textbox,fieldValidations: [Validators.required]  },
        { prop: 'fullName', name: 'Full name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required, this.formsService.nameValidator]},
        { prop: 'email', name: 'Email', fieldType: FieldTypes.Textbox,fieldValidations:[Validators.email, Validators.required] },
        { prop: 'mobile', name: 'Mobile', fieldType: FieldTypes.Textbox,fieldValidations:[this.formsService.telehponeValidator]},
        { prop: 'address', name: 'Address', fieldType: FieldTypes.Textbox,fieldValidations: [Validators.required] },
        { prop: 'note', name: 'Note', fieldType: FieldTypes.Textarea },
        { prop: 'tourCustomers', name: 'Tour Customer',cellTemplate:this.customersTemplate,
          subTableColumn:[
            { prop: 'tourBookingId', name: 'Tour Booking Id', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required]  },
            { prop: 'touristType', name: 'Tourist Type', fieldValidations: [Validators.required], fieldType: FieldTypes.Select,
              fieldOptions:[
                {key:0,value:'Adult'},
                {key:1,value:'Children'},
                {key:2,value:'Kid'},
              ]},
            { prop: 'fullName', name: 'Full Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required,this.formsService.nameValidator ]  },
            { prop: 'gender', name: 'Gender', fieldType: FieldTypes.Textbox },
            { prop: 'birthDay', name: 'Birthday', fieldType: FieldTypes.Textbox}
          ]
        },
        { prop: 'bookingPrices', name: 'Booking Prices',cellTemplate:this.bookingPricesTemplate,
          subTableColumn:[
            { prop: 'tourBookingId', name: 'Tour Booking Id', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required]  },
            { prop: 'touristType', name: 'Tourist Type', fieldValidations: [Validators.required], fieldType: FieldTypes.Select,
              fieldOptions:[
                {key:0,value:'Adult'},
                {key:1,value:'Children'},
                {key:2,value:'Kid'},
              ]},
            { prop: 'price', name: 'Price', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required]  }
          ]
        }
      ]
    };
    this.chosenEdit=true;
    this.table.updateData('api/TourBooking');
  }
 clickCensorship() {
   this.options = {
     id:'tour-booking',
     title: 'Censorship tour bookings',
     apiUrl: 'api/TourBooking/Sensorships',
     disableEditing:true,
     enableCensorship:true,
     disablechangetour: true,
     disableviewContact: true,
     columns: [
       { prop: 'id', name: 'Id'},
       { prop: 'tourId', name: 'Tour Id', fieldType: FieldTypes.Textbox,fieldValidations: [Validators.required]  },
       { prop: 'fullName', name: 'Full name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
       { prop: 'email', name: 'Email', fieldType: FieldTypes.Textbox },
       { prop: 'mobile', name: 'Mobile', fieldType: FieldTypes.Textbox },
       { prop: 'address', name: 'Address', fieldType: FieldTypes.Textbox },
       { prop: 'note', name: 'Note', fieldType: FieldTypes.Textarea },
       { prop: 'tourCustomers', name: 'Tour Customer',cellTemplate:this.customersTemplate,
         subTableColumn:[
           { prop: 'tourBookingId', name: 'Tour Booking Id', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required]  },
           { prop: 'touristType', name: 'Tourist Type', fieldValidations: [Validators.required], fieldType: FieldTypes.Select,
             fieldOptions:[
               {key:0,value:'Adult'},
               {key:1,value:'Children'},
               {key:2,value:'Kid'},
             ]},
           { prop: 'fullName', name: 'Full Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required]  },
           { prop: 'gender', name: 'Gender', fieldType: FieldTypes.Textbox },
           { prop: 'birthDay', name: 'Birthday', fieldType: FieldTypes.Textbox}
         ]
       },
       { prop: 'bookingPrices', name: 'Booking Prices',cellTemplate:this.bookingPricesTemplate,
         subTableColumn:[
           { prop: 'tourBookingId', name: 'Tour Booking Id', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required]  },
           { prop: 'touristType', name: 'Tourist Type', fieldValidations: [Validators.required], fieldType: FieldTypes.Select,
             fieldOptions:[
               {key:0,value:'Adult'},
               {key:1,value:'Children'},
               {key:2,value:'Kid'},
             ]},
           { prop: 'price', name: 'Price', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required]  }
         ]
       }
     ]
   };
   this.chosenEdit=false;
   this.table.updateData('api/TourBooking/Sensorships');
 }
}

import {Component, HostBinding, OnInit, ViewChild} from '@angular/core';
import {FieldTypes, IAppTableOptions} from "@app/models";
import {AppTableComponent} from "@app/shared";
import {Validators} from "@angular/forms";

@Component({
  selector: 'appc-manage-contacts-staff',
  templateUrl: './manage-contacts-staff.component.html',
  styleUrls: ['./manage-contacts-staff.component.scss']
})
export class ManageContactsStaffComponent implements OnInit {
  @HostBinding('class')
  elementClass = 'col-lg-10 col-md-9 bg-light content';
  options: IAppTableOptions<Comunication>;
  @ViewChild('table', { static: true }) table:AppTableComponent;
  constructor() { }
  ngOnInit() {
    this.options = {
      title: 'Tour categories',
      apiUrl: 'api/contact',
      disableFilter: true,
      disableDelete:true,
      disableUpdate:true,
      disableEditing: true,
      disablechangetour: true,
      columns: [
        { prop: 'fullName', name: 'FullName', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'email', name: 'Email', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'address', name: 'Address', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'phone', name: 'Phone', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'title', name: 'Title', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'content', name: 'Content', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
        { prop: 'information', name: 'Information', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
      ]
    };
    this.table.updateData('api/contact');
  }
}

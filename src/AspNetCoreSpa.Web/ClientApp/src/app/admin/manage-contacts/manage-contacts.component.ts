import {Component, HostBinding, OnInit} from '@angular/core';
import {FieldTypes, IAppTableOptions} from "@app/models";
import {Validators} from "@angular/forms";

@Component({
  selector: 'appc-manage-contacts',
  templateUrl: './manage-contacts.component.html',
  styleUrls: ['./manage-contacts.component.scss']
})
export class ManageContactsComponent implements OnInit {
  @HostBinding('class')
  elementClass = 'col-lg-10 col-md-9 bg-light content';
  options: IAppTableOptions<Comunication>;

  constructor() { }

  ngOnInit() {
    
    this.options = {
      title: 'Tour categories',
      apiUrl: 'api/contact',
      disableDelete:true,
      disableUpdate:true,
      disableEditing: true,
      disableFilter: true,
      columns: [
        { prop: 'FullName', name: 'FullName', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'Email', name: 'Email', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'Address', name: 'Address', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'Phone', name: 'Phone', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'Title', name: 'Title', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'Content', name: 'Content', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'Information', name: 'Information', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
      ]
    };
  }

}

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
        { prop: 'fullName', name: 'FullName', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'email', name: 'Email', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'address', name: 'Address', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'phone', name: 'Phone', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'title', name: 'Title', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'content', name: 'Content', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'information', name: 'Information', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
      ]
    };
  }

}

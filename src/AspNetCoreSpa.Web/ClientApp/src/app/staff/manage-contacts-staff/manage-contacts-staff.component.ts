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
  chosenEdit = true;
  constructor() { }
  ngOnInit(){
  this.clickUpdateContact();
  this.clickChecked();
  }
  clickUpdateContact() {
    this.options = {
      title: 'Contacts',
      apiUrl: 'api/contact',
      disableFilter: true,
      disableDelete:true,
      disablechangetour: true,
      disableFilterDepartue: true,
      disableFilterName: true,
      disableView: true,
      columns: [
        { prop: 'fullName', name: 'FullName', fieldType: FieldTypes.Textbox},
        { prop: 'email', name: 'Email', fieldType: FieldTypes.Textbox},
        { prop: 'address', name: 'Address', fieldType: FieldTypes.Textbox},
        { prop: 'phone', name: 'Phone', fieldType: FieldTypes.Textbox},
        { prop: 'title', name: 'Title', fieldType: FieldTypes.Textbox },
        { prop: 'content', name: 'Content', fieldType: FieldTypes.Textarea},
        { prop: 'information', name: 'Information', fieldType: FieldTypes.Textbox},
        { prop: 'checked', name: 'Checked', fieldType: FieldTypes.Checkbox, fieldValidations: [Validators.required]},
        { prop: 'note', name: 'Note', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
      ]
    };
    this.chosenEdit=true;
    this.table.updateData('api/contact');
  }
  
  clickChecked() {
    this.options = {
      title: 'Contacts',
      apiUrl: 'api/contact/censorship',
      disableFilter: true,
      disableDelete:true,
      disablechangetour: true,
      disableFilterDepartue: true,
      disableFilterName: true,
      disableEditing: true,
      columns: [
        { prop: 'fullName', name: 'Full Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'email', name: 'Email', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'address', name: 'Address', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'phone', name: 'Phone', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'title', name: 'Title', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'content', name: 'Content', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
        { prop: 'information', name: 'Information', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'note', name: 'Note', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
      ]
    };
    this.chosenEdit=false;
    this.table.updateData('api/contact/censorship');
  }
}

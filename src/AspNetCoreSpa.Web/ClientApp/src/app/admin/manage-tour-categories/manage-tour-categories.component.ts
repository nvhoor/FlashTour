import {Component, HostBinding, OnInit} from '@angular/core';
import {FieldTypes, IAppTableOptions} from "@app/models";
import {Validators} from "@angular/forms";
@Component({
  selector: 'appc-manage-tour-categories',
  templateUrl: './manage-tour-categories.component.html',
  styleUrls: ['./manage-tour-categories.component.scss']
})
export class ManageTourCategoriesComponent implements OnInit {
  @HostBinding('class')
  elementClass = 'col-lg-10 col-md-9 bg-light content';
  options: IAppTableOptions<Comunication>;
  constructor() { }

  ngOnInit() {
    this.options = {
      title: 'Tour categories',
      apiUrl: 'api/tourcategory',
      columns: [
        { prop: 'name', name: 'Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'Description', name: 'Description', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'Image', name: 'Image', fieldType: FieldTypes.FileUpload, fieldValidations: [Validators.required] },
      ]
    };
  }

}

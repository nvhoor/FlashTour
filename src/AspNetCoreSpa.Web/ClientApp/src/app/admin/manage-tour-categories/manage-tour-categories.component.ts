import {Component, HostBinding, OnInit, ViewChild} from '@angular/core';
import {FieldTypes, IAppTableOptions} from "@app/models";
import {Validators} from "@angular/forms";
import {AppTableComponent} from "@app/shared";
@Component({
  selector: 'appc-manage-tour-categories',
  templateUrl: './manage-tour-categories.component.html',
  styleUrls: ['./manage-tour-categories.component.scss']
})
export class ManageTourCategoriesComponent implements OnInit {
  @HostBinding('class')
  elementClass = 'col-lg-10 col-md-9 bg-light content';
  options: IAppTableOptions<Comunication>;
  @ViewChild('table', { static: true }) table:AppTableComponent;
  constructor() { }

  ngOnInit() {
    this.options = {
      title: 'Tour categories',
      apiUrl: 'api/tourcategory',
      disableFilter: true,
      disablechangetour: true,
      disableviewContact: true,
      columns: [
        { prop: 'name', name: 'Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'description', name: 'Description', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'image', name: 'Image', fieldType: FieldTypes.FileUpload, fieldValidations: [Validators.required] },
      ]
    };
    this.table.updateData('api/tourcategory');
  }

}

import {Component, HostBinding, OnInit, ViewChild} from '@angular/core';
import {FieldTypes, IAppTableOptions} from "@app/models";
import {Validators} from "@angular/forms";
import {AppTableComponent,FormsService,} from "@app/shared";
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
  constructor(
      private formsService: FormsService,
  ) { }

  ngOnInit() {
    this.options = {
      title: 'Tour categories',
      apiUrl: 'api/tourcategory',
      disableFilter: true,
      disablechangetour: true,
      disableviewContact: true,
      disableFilterDepartue: true,
      disableFilterName: true,
      disableDelete: true,
      columns: [
        { prop: 'name', name: 'Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required, this.formsService.nameValidator] },
        { prop: 'description', name: 'Description', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'image', name: 'Image', fieldType: FieldTypes.FileUpload, fieldValidations: [Validators.required] ,imgSrcUrl:'api/TourCategory/UploadImage' },
      ]
    };
    this.table.updateData('api/tourcategory');
  }

}

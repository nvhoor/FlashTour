import {Component, HostBinding, OnInit} from '@angular/core';
import {FieldTypes, IAppTableOptions} from "@app/models";
import {Validators} from "@angular/forms";

@Component({
  selector: 'appc-manage-banners',
  templateUrl: './manage-banners.component.html',
  styleUrls: ['./manage-banners.component.scss']
})
export class ManageBannersComponent implements OnInit {
  @HostBinding('class')
  elementClass = 'col-lg-10 col-md-9 bg-light content';
  options: IAppTableOptions<Comunication>;
  constructor() { }

  ngOnInit() {
    this.options = {
      title: 'Banner',
      apiUrl: 'api/banner',
      disableFilter: true,
      disablechangetour: true,
      disableviewContact: true,
      columns: [
        { prop: 'name', name: 'Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'description', name: 'Description', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'postId', name: 'PostId',
          fieldType: FieldTypes.Select,
          fieldOptions:[
            {key:"ecacca37-bf93-46e7-84a8-3f1f246bb860",value:'Khuyến Mãi siêu hot'},
            {key:"ecacca37-bf93-46e7-84a8-3f1f246bb860",value:'Flash Deal'},
          ]},
        { prop: 'image', name: 'Image', fieldType: FieldTypes.Textbox,fieldValidations: [Validators.required] },
      ]};
  }

}

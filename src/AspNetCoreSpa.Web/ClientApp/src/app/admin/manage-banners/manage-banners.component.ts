import {Component, HostBinding, Inject, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {FieldTypes, IAppTableOptions, IFieldConfig, IOption} from "@app/models";
import {Validators} from "@angular/forms";
import {clone} from 'lodash';
import {AppFormComponent, AppTableComponent} from "@app/shared";
import {DataService, ModalService} from "@app/services";
import {ToastrService} from "@app/toastr";

@Component({
  selector: 'appc-manage-banners',
  templateUrl: './manage-banners.component.html',
  styleUrls: ['./manage-banners.component.scss']
})
export class ManageBannersComponent implements OnInit {
  @HostBinding('class')
  elementClass = 'col-lg-10 col-md-9 bg-light content';
  options: IAppTableOptions<Comunication>;
  postFieldOption:IOption[];
  @ViewChild('postTemplate', { static: true }) postTemplate: TemplateRef<any>;
  constructor(
      @Inject("BASE_URL") private baseUrl: string,
      private modalService: ModalService,
      private _dataService:DataService,
      private toastr: ToastrService,) { }

  ngOnInit() {
    this.options={apiUrl:'api/banner'};
    var data = this._dataService.getFull<Post>(`${this.baseUrl}api/post`);
    let that = this;
    data.subscribe((result) => {
      console.log("Post: ",JSON.stringify(result.body));
      var fieldOptions=[];
      result.body.forEach((d, i) => {
        fieldOptions.push({
          key:d.id,
          value:d.name
        });
      });
      this.postFieldOption=fieldOptions;
      this.newBanner();
    });
}
   newBanner(){
    this.options = {
    title: 'Banner',
    apiUrl: 'api/banner',
    disableFilter: true,
    disablechangetour: true,
    disableviewContact: true,
    columns: [
      { prop: 'name', name: 'Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
      { prop: 'description', name: 'Description', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] }, 
        { prop: 'postId', name: 'Post Name', fieldType: FieldTypes.Select,
            fieldOptions: this.postFieldOption,cellTemplate: this.postTemplate},  
      { prop: 'image', name: 'Image', fieldType: FieldTypes.FileUpload,fieldValidations: [Validators.required] },
    ]};
  }
}

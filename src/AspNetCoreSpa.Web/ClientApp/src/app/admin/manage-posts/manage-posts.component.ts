import {Component, HostBinding, Inject, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {FieldTypes, IAppTableOptions, IOption} from "@app/models";
import {DataService, ModalService} from "@app/services";
import {ToastrService} from "@app/toastr";
import {Validators} from "@angular/forms";
import {AppTableComponent} from "@app/shared";

@Component({
  selector: 'appc-manage-posts',
  templateUrl: './manage-posts.component.html',
  styleUrls: ['./manage-posts.component.scss']
})
export class ManagePostsComponent implements OnInit {

  @HostBinding('class')
  elementClass = 'col-lg-10 col-md-9 bg-light content';
  options: IAppTableOptions<Comunication>;
  postCateFieldOption:IOption[];
  @ViewChild('postCateTemplate', { static: true }) postCateTemplate: TemplateRef<any>;
  @ViewChild('table', { static: true }) table:AppTableComponent;
  chosenEdit= true;
  constructor(
      @Inject("BASE_URL") private baseUrl: string,
      private modalService: ModalService,
      private _dataService:DataService,
      private toastr: ToastrService,) { }
  ngOnInit() {
    this.options={apiUrl:'api/post'};
    var data = this._dataService.getFull<PostCate>(`${this.baseUrl}api/postcategory`);
    let that = this;
    data.subscribe((result) => {
      console.log("Post Cate Name: ",JSON.stringify(result.body));
      var fieldOptions=[];
      result.body.forEach((d, i) => {
        fieldOptions.push({
          key:d.id,
          value:d.name
        });
      });
      this.postCateFieldOption=fieldOptions;
      this.newPost();
    });
  }
  newPost(){
    this.options = {
      title: 'Posts',
      apiUrl: 'api/post',
      disableFilter: true,
      disablechangetour: true,
      disableviewContact: true,
      enablepostCensorship: false,
      disableFilterDepartue: true,
      columns: [
        { prop: 'name', name: 'Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'postContent', name: 'PostContent', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
        { prop: 'description', name: 'Description', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'image', name: 'Image', fieldType: FieldTypes.FileUpload,fieldValidations: [Validators.required] ,imgSrcUrl:'api/post/UploadImage' },
        { prop: 'metaDescription', name: 'MetaDescription', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
        { prop: 'metaKeyWord', name: 'MetaKeyWord', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
        { prop: 'alias', name: 'Alias', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'updatedAt', name: 'Updated At', fieldType: FieldTypes.Date },
        { prop: 'postCategoryId', name: 'PostCategory', fieldType: FieldTypes.Select,
           fieldOptions: this.postCateFieldOption,cellTemplate: this.postCateTemplate},
      ]};
    this.table.updateData('api/post');
  }

  clickCensorshipPost(){
    this.options = {
      title: 'Posts',
      apiUrl: 'api/post/cencershippost',
      disableFilter: true,
      disablechangetour: true,
      disableviewContact: false,
      enablepostCensorship: true,
      disableEditing: true,
      disableFilterDepartue: true,
      columns: [
        { prop: 'name', name: 'Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'postContent', name: 'PostContent', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
        { prop: 'description', name: 'Description', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'image', name: 'Image', fieldType: FieldTypes.FileUpload,fieldValidations: [Validators.required] ,imgSrcUrl:'api/post/UploadImage' },
        { prop: 'metaDescription', name: 'MetaDescription', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
        { prop: 'metaKeyWord', name: 'MetaKeyWord', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
        { prop: 'alias', name: 'Alias', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'updatedAt', name: 'Updated At', fieldType: FieldTypes.Date },
        { prop: 'postCategoryId', name: 'PostCategory', fieldType: FieldTypes.Select,
          fieldOptions: this.postCateFieldOption,cellTemplate: this.postCateTemplate},
      ]};
    this.table.updateData('api/post/cencershippost');
  }
  
}

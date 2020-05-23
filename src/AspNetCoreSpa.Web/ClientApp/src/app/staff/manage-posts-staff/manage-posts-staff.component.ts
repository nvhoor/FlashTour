import {Component, HostBinding, Inject, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {FieldTypes, IAppTableOptions, IOption} from "@app/models";
import {Validators} from "@angular/forms";
import {AppTableComponent, FormsService} from "@app/shared";
import {DataService, ModalService} from "@app/services";
import {ToastrService} from "@app/toastr";

@Component({
  selector: 'appc-manage-posts-staff',
  templateUrl: './manage-posts-staff.component.html',
  styleUrls: ['./manage-posts-staff.component.scss']
})
export class ManagePostsStaffComponent implements OnInit {

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
      private toastr: ToastrService,
      private formsService: FormsService,
  ) { }
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
      title: 'Post',
      apiUrl: 'api/post',
      disableFilter: false,
      disablechangetour: true,
      disableviewContact: true,
      enablepostCensorship: false,
      disableFilterDepartue: true,
      disableFilterName: true,
      columns: [
        { prop: 'name', name: 'Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required,this.formsService.nameValidator] },
        { prop: 'postContent', name: 'PostContent', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
        { prop: 'description', name: 'Description', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'image', name: 'Image', fieldType: FieldTypes.FileUpload,fieldValidations: [Validators.required],imgSrcUrl:'api/post/UploadImage'},
        { prop: 'metaDescription', name: 'MetaDescription', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
        { prop: 'metaKeyWord', name: 'MetaKeyWord', fieldType: FieldTypes.Textarea, fieldValidations: [Validators.required] },
        { prop: 'alias', name: 'Alias', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'postCategoryId', name: 'PostCategory', fieldType: FieldTypes.Select,
          fieldOptions: this.postCateFieldOption,cellTemplate: this.postCateTemplate,fieldValidations: [Validators.required]},
      ]};
    this.table.updateData('api/post');
  }
}

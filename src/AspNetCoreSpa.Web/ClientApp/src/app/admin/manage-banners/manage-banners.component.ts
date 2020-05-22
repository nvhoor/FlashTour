import {Component, HostBinding, Inject, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {FieldTypes, IAppTableOptions, IFieldConfig, IOption} from "@app/models";
import {Validators} from "@angular/forms";
import {clone} from 'lodash';
import {AppFormComponent, AppTableComponent,FormsService} from "@app/shared";
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
    @ViewChild('table', { static: true }) table:AppTableComponent;
    chosenEdit = true;
    constructor(
        @Inject("BASE_URL") private baseUrl: string,
        private modalService: ModalService,
        private _dataService:DataService,
        private toastr: ToastrService,
        private formsService: FormsService,) { }

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
            title: 'Banners',
            apiUrl: 'api/banner',
            disableFilter: true,
            disablechangetour: true,
            disableviewContact: true,
            disableFilterDepartue: true,
            enablebannerCensorship: false,
            disableFilterName: true,
            columns: [
                { prop: 'name', name: 'Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required, this.formsService.nameValidator] },
                { prop: 'description', name: 'Description', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
                { prop: 'postId', name: 'Post Name', fieldType: FieldTypes.Select,
                    fieldOptions: this.postFieldOption,cellTemplate: this.postTemplate},
                { prop: 'image', name: 'Image', fieldType: FieldTypes.FileUpload,fieldValidations: [Validators.required] ,imgSrcUrl:'api/Banner/UploadImage'},
            ]};
        this.table.updateData('api/banner');
    }

    clickCensorshipBanner() {
        this.options = {
            title: 'Banners',
            apiUrl: 'api/banner/censorship',
            disableFilter: true,
            disablechangetour: true,
            disableviewContact: false,
            disableEditing: true,
            disableFilterDepartue: true,
            enablebannerCensorship: true,
            disableFilterName: true,
            columns: [
                { prop: 'name', name: 'Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
                { prop: 'description', name: 'Description', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required, Validators.maxLength(500), Validators.minLength(10)]},
                { prop: 'postId', name: 'Post Name', fieldType: FieldTypes.Select,
                    fieldOptions: this.postFieldOption,cellTemplate: this.postTemplate},
                { prop: 'image', name: 'Image', fieldType: FieldTypes.FileUpload,fieldValidations: [Validators.required],imgSrcUrl:'api/Banner/UploadImage' },
            ]};
        this.table.updateData('api/banner/censorship');
    }
}

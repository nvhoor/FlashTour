import { Component } from '@angular/core';

import { FieldBaseComponent } from '../field-base';
import {HttpEventType} from "@angular/common/http";
declare var $: any;

@Component({
    selector: 'app-form-file',
    styleUrls: ['form-file.component.scss'],
    templateUrl: 'form-file.component.html'
})
export class FormFileComponent extends FieldBaseComponent{
    public progress: number;
    public message: string;
    onFileChange($event) {
        let file = $event.target.files[0]; // <--- File Object for future use.
        this.formGroup.controls[this.config.name].setValue(file ? file.name : ''); // <-- Set Value for Validation
            var fileName = $event.target.value.split("\\").pop();
        $(".custom-file-label").addClass("selected").html(fileName);
        this.readURL($event.target);
       this.formGroup.value.image=fileName;
    }
    public uploadFile = (files) => {
        if (files.length === 0) {
            return;
        }
        let fileToUpload = <File>files[0];
        const formData = new FormData();
        formData.append('file', fileToUpload, fileToUpload.name);

        this.http.post(`${this.baseUrl+this.config.imgSrcUrl}`, formData, {reportProgress: true, observe: 'events'})
            .subscribe(event => {
                if (event.type === HttpEventType.UploadProgress)
                    this.progress = Math.round(100 * event.loaded / event.total);
                else if (event.type === HttpEventType.Response) {
                    this.message = 'Upload success.';
                }
            });
    };
    public readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function(e) {
                $('#image-preview').attr('src', e.target.result);
            }.bind(this);
            reader.readAsDataURL(input.files[0]); // convert to base64 string
        }
    }
    getFolderImage(){
        console.log("getFolderImage",this.config.imgSrcUrl);
        if(this.config.imgSrcUrl){
            return this.config.imgSrcUrl.split('/')[1].toLowerCase()+"s"; 
        }else{
            return "tours";
        }
      
    }
}
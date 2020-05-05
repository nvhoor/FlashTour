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
        
    }
    public uploadFile = (files) => {
        if (files.length === 0) {
            return;
        }

        let fileToUpload = <File>files[0];
        const formData = new FormData();
        formData.append('file', fileToUpload, fileToUpload.name);

        this.http.post('https://localhost:5005/api/Tour/UploadImage', formData, {reportProgress: true, observe: 'events'})
            .subscribe(event => {
                if (event.type === HttpEventType.UploadProgress)
                    this.progress = Math.round(100 * event.loaded / event.total);
                else if (event.type === HttpEventType.Response) {
                    this.message = 'Upload success.';
                }
            });
    }
}
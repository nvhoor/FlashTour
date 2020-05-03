import { Component } from '@angular/core';

import { FieldBaseComponent } from '../field-base';
declare var $: any;
@Component({
    selector: 'app-form-file',
    styleUrls: ['form-file.component.scss'],
    templateUrl: 'form-file.component.html'
})
export class FormFileComponent extends FieldBaseComponent{
    onFileChange($event) {
        let file = $event.target.files[0]; // <--- File Object for future use.
        this.formGroup.controls[this.config.name].setValue(file ? file.name : ''); // <-- Set Value for Validation
            console.log("afsdhgdfhgfjfgjfgh");
            var fileName = $event.target.val().split("\\").pop();
        $event.target.siblings(".custom-file-label").addClass("selected").html(fileName);
        
    }
}
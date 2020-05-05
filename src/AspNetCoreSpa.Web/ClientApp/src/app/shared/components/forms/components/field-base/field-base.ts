import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { format } from 'date-fns';

import { IFieldConfig, Field } from '@app/models';
import { AppFormComponent } from '../form';
import { FormsService } from '../../forms.service';
import {HttpClient} from "@angular/common/http";

@Injectable()
export abstract class FieldBaseComponent implements Field {
    config: IFieldConfig;
    constructor(
        public fc: AppFormComponent,
        private formService: FormsService,
        public http: HttpClient) { }
    get formGroup(): FormGroup {
        return this.fc.form;
    }

    showAsterisk(config: IFieldConfig): boolean {
        return this.formService.showIndicator(config);
    }
}

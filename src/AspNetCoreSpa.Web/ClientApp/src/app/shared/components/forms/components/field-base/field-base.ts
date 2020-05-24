import {Inject, Injectable} from '@angular/core';
import { FormGroup } from '@angular/forms';
import { format } from 'date-fns';

import { IFieldConfig, Field } from '@app/models';
import { AppFormComponent } from '../form';
import { FormsService } from '../../forms.service';
import {HttpClient} from "@angular/common/http";
import {DataService} from "@app/services";

@Injectable()
export abstract class FieldBaseComponent implements Field {
    config: IFieldConfig;
    constructor(
        @Inject("BASE_URL") public baseUrl: string,
        public fc: AppFormComponent,
        private formService: FormsService,
        public http: HttpClient,
        public dataService:DataService) { }
    get formGroup(): FormGroup {
        return this.fc.form;
    }

    showAsterisk(config: IFieldConfig): boolean {
        return this.formService.showIndicator(config);
    }
}

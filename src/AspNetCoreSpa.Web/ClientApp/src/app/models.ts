import { InjectionToken, TemplateRef } from '@angular/core';
import { ValidatorFn } from '@angular/forms';
import { TableColumn } from '@swimlane/ngx-datatable';

export const COOKIES = new InjectionToken<string>('COOKIES');
export interface KeyValuePair<T> {
    key: string;
    value: T;
}

export interface ISocialLogins {
    loginProvider: string;
    providerKey: string;
    providerDisplayName: string;
    active: boolean;
}

export interface ITwoFactorModel {
    hasAuthenticator: boolean;

    recoveryCodesLeft: number;

    is2faEnabled: boolean;
}


export interface IEnableAuthenticatorModel {
    code: string;
    sharedKey: string;
    authenticatorUri: string;
}

export interface ITableColumn extends TableColumn {
    fieldType?: FieldTypes;
    fieldOptions?: IOption[];
    fieldValidations?: ValidatorFn[];
    onSubmit?:Function;
    subTableColumn?:ITableColumn[];
}

export interface IAppTableOptions<T> {
    disableView?: boolean;
    disableviewContact?: boolean;
    disablechangetour?: boolean;
    enabletourCensorship?: boolean;
    id?:string;
    title?: string;
    rows?: Array<T>;
    columns?: ITableColumn[];
    disableEditing?: boolean;
    disableUpdate?: boolean;
    disableDelete?: boolean;
    disableFilter?: boolean;
    enableCensorship?: boolean;
    apiUrl?: string;
    detailsTemplate?: TemplateRef<any>;
}

export interface IModalOptions {
    title: string;
    message?: string;
    template?: any;
}
export enum FieldTypes {
    Textbox = 'input',
    FileUpload = 'file',
    Password = 'password',
    Email = 'email',
    Number = 'number',
    Date = 'date',
    Time = 'time',
    Textarea = 'textarea',
    Radiolist = 'radiolist',
    Select = 'select',
    Checkbox = 'checkbox',
    Checkboxlist = 'checkboxlist',
    Button = 'button'
}
export enum SubDataTable {
    BOOKING_PRICE='booking-price',
    TOUR_CUSTOMER='tour-customer'
}
export interface Field {
    config: IFieldConfig;
}
export interface IFieldConfig {
    name: string;
    label?: string;
    disabled?: boolean;
    options?: IOption[];
    placeholder?: string;
    type: FieldTypes;
    validation?: ValidatorFn[];
    value?: any;
    onSubmit?: Function;
    errorMessages?: Object;
}

export interface IOption {
    key: string | number;
    value: string;
    selected?: boolean;
}
export interface Contact {
    information:string,
    fullName:string,
    email:string,
    phone:string,
    address:string,
    title:string,
    content:string,
}
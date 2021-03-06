import { Component, Input, OnChanges, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import {FormGroup, FormControl, FormBuilder, NgForm, FormArray} from '@angular/forms';

import {FieldTypes, IFieldConfig} from '@app/models';
import {DataService, ModalService} from "@app/services";
import {ToastrService} from "@app/toastr";

@Component({
    exportAs: 'appForm',
    selector: 'app-form',
    styleUrls: ['form.component.scss'],
    templateUrl: './form.component.html'
})
export class AppFormComponent implements OnChanges, OnInit, AfterViewInit {
    @ViewChild('formRef', { static: true }) ngForm: NgForm;
    @Input() config: IFieldConfig[] = [];
    @Input() model: any;
    @Input() fullWidth: boolean;
    @Input() customerData?:Customer[];
    @Input() imagesData?:ImageTour[];
    @Input() priceData?:BookingPrice[];
    @Input() pricesData?:Price[];
    @Input() tourproData?:TourProgram[];

    @Input() tourIdChosen?:string;
    form: FormGroup;
    listCustomButtonName=["buttonCreateCustomer","buttonUpdateCustomer","buttonCreatePrice","buttonUpdatePrice","buttonCreateProgram","buttonUpdateProgram","buttonAddImage"];
    currentSelectedRowIdx=0;
    get controls() { return this.config.filter(({ type }) => type !== FieldTypes.Button); }
    get changes() { return this.form.valueChanges; }
    get valid() { return this.form.valid; }
    get value() { return this.form.value; }
    get submitted() { return this.ngForm.submitted; }
    get getCustomerData() { return this.customerData; }
    get getPriceData() { return this.priceData; }

    constructor(private fb: FormBuilder,private dataService: DataService,private toastr: ToastrService,
                private modalService: ModalService,) { }
    ngOnInit() {
        this.form = this.createGroup();
        this.config.forEach(fieldConfig=>{
            //console.log("fieldConfig",JSON.stringify(fieldConfig));
            if(this.listCustomButtonName.indexOf(fieldConfig.name)!=-1){
                this.addCustomFunction(fieldConfig);
            }
        });
    }
    ngAfterViewInit() {
        if (this.model) {
            setTimeout(() => {
                this.form.patchValue(this.model);
            }, 0);
        }
    }
    ngOnChanges() {
        if (this.form) {
            const controls = Object.keys(this.form.controls);
            const configControls = this.controls.map((item) => item.name);

            controls
                .filter((control) => !configControls.includes(control))
                .forEach((control) => this.form.removeControl(control));

            configControls
                .filter((control) => !controls.includes(control))
                .forEach((name) => {
                    const config = this.config.find((control) => control.name === name);
                    let control=this.createControl(config);
                    this.form.addControl(name,control );
                });
        }
    }
    createGroup() {
        const group = this.fb.group({});
        this.controls.forEach(control => group.addControl(control.name, this.createControl(control)));
        return group;
    }
    createControl(config: IFieldConfig): FormControl | FormArray {
        const { disabled, validation, value, options } = config;
        if (config.type === FieldTypes.Checkboxlist) {
            return this.fb.array(
                options.map((item, index) => {
                    return this.fb.control(item.selected);
                }), config.validation ? config.validation[0] : null
            );
        }
        return this.fb.control({ disabled, value }, validation);
    }
    setDisabled(name: string, disable: boolean) {
        if (this.form.controls[name]) {
            const method = disable ? 'disable' : 'enable';
            this.form.controls[name][method]();
            return;
        }

        this.config = this.config.map((item) => {
            if (item.name === name) {
                item.disabled = disable;
            }
            return item;
        });
    }
    setValue(name: string, value: any) {
        this.form.controls[name].setValue(value, { emitEvent: true });
    }
    reset() {
        this.ngForm.resetForm();
    }
    validate() {
        Object.keys(this.form.controls).forEach(field => {
            const control = this.form.get(field);
            control.markAsTouched({ onlySelf: true });
        });
    }

    private addCustomFunction(config) {
        switch (config.name) {
            case "buttonCreateCustomer":
                config.onSubmit=this.createTourCustomer.bind(this);
                break;
            case "buttonUpdateCustomer":
                config.onSubmit=this.updateTourCustomer.bind(this);
                break;
            case "buttonCreatePrice":
                config.onSubmit=this.createTourPrice.bind(this);
                break;
            case "buttonUpdatePrice":
                config.onSubmit=this.updateTourPrice.bind(this);
                break;
            case "buttonCreateProgram":
                config.onSubmit=this.createTourProgram.bind(this);
                break;
            case "buttonUpdateProgram":
                config.onSubmit=this.updateTourProgram.bind(this);
                break;
            case "buttonAddImage":
                config.onSubmit=this.updateTourImages.bind(this);
                break;
                
        }

    }
    //Tourbooking
    onClickSelectCustomer(idx) {
        this.currentSelectedRowIdx=idx;
        this.model=this.customerData[idx];
        this.setValue("touristType",this.customerData[idx].touristType);
        this.setValue("fullName",this.customerData[idx].fullName);
        this.setValue("gender",this.customerData[idx].gender);
        this.setValue("birthDay",this.customerData[idx].birthDay);
    }
    onClickSelectImages(idx) {
        this.currentSelectedRowIdx=idx;
        this.setValue("image",this.imagesData[idx].image);
    }
    onClickDeleteImage(idx) {
        console.log("onClickDeleteImage",this.imagesData[idx].image);
        this.dataService.put(`api/Tour/DeleteImage/${this.imagesData[idx].tourId}`,
            { tourId: this.imagesData[idx].tourId,image:this.imagesData[idx].image })
            .subscribe(res => {
                var imgEx=this.imagesData[idx].image;
                this.imagesData= this.imagesData.filter(image=>{
                    return image.image!=imgEx;
                });
                console.log("onClickDeleteImage success!")
            });
    }
    updateTourImages(){
        console.log("updateTourImages",this.value);
        this.dataService.put(`api/Tour/AddImage/${this.tourIdChosen}`,
            this.value )
            .subscribe(res => {
                this.imagesData.push({
                   tourId: this.tourIdChosen,
                    image: this.value.image
                });
                console.log("updateTourImages success!")
            });
    }

    onClickSelectBookingPrice(idx) {
        this.currentSelectedRowIdx=idx;
        this.model=this.customerData[idx];
        this.setValue("tourBookingId",this.priceData[idx].tourBookingId);
        this.setValue("touristType",this.priceData[idx].touristType);
        this.setValue("price",this.priceData[idx].price);
    }
    updateTourCustomer(){
        console.log("updateTourCustomer",JSON.stringify( this.value));
        this.dataService.put(`api/TourCustomer/${this.model.id}`, { ... this.value })
            .subscribe(res => {
                let id=this.customerData[this.currentSelectedRowIdx].id;
                this.customerData[this.currentSelectedRowIdx]=this.value;
                this.customerData[this.currentSelectedRowIdx].id=id;
                this.toastr.success('Updated successfully.', 'Success');
            });
    }
    createTourCustomer(){
        console.log("createTourCustomer",JSON.stringify( this.value));
        this.dataService.post(`api/TourCustomer`, { ...this.value})
            .subscribe(res => {
                this.dataService.get<any[]>(`api/TourCustomer/ByTourBooking`,{tourBookingId:this.value.tourBookingId,
                touristType:this.value.touristType})
                    .subscribe(data => {
                        console.log("data",JSON.stringify(data));
                        this.customerData=this.customerData.filter(x=>x.touristType!=this.value.touristType);
                        this.customerData=this.customerData.concat(data);
                        this.toastr.success('Created successfully.', 'Success');
                    });
            });
    }
    /* Tour Prices*/
    onClickSelectTourPrice(idx){
        this.currentSelectedRowIdx=idx;
        this.model=this.pricesData[idx];
        this.setValue("name",this.pricesData[idx].name);
        this.setValue("originalPrice",this.pricesData[idx].originalPrice);
        this.setValue("promotionPrice",this.pricesData[idx].promotionPrice);
        this.setValue("startDatePro",this.pricesData[idx].startDatePro);
        this.setValue("endDatePro",this.pricesData[idx].endDatePro);
        this.setValue("touristType",this.pricesData[idx].touristType);
    }
    updateTourPrice(){
        console.log("updateTourPrice",JSON.stringify( this.value));
        this.dataService.put(`api/price/${this.model.id}`, { ... this.value })
            .subscribe(res => {
                let id=this.pricesData[this.currentSelectedRowIdx].tourId;
                this.pricesData[this.currentSelectedRowIdx]=this.value;
                this.pricesData[this.currentSelectedRowIdx].tourId=id;
                this.toastr.success('Updated successfully.', 'Success');
            });
    }
    createTourPrice(){
        console.log("createTourPrice",JSON.stringify( this.value));
        this.dataService.post(`api/price`, { ...this.value})
            .subscribe(res => {
                this.dataService.get<any[]>(`api/price/ByTourId`,{tourId:this.value.tourId,
                    touristType:this.value.touristType})
                    .subscribe(data => {
                        console.log("data",JSON.stringify(data));
                        this.pricesData=this.pricesData.filter(x=>x.touristType!=this.value.touristType);
                        this.pricesData=this.pricesData.concat(data);
                        this.toastr.success('Created successfully.', 'Success');
                    });
            });
    }
    // Tour Program
    onClickSelectTourPro(idx) {
        this.currentSelectedRowIdx=idx;
        this.model=this.tourproData[idx];
        this.setValue("date",this.tourproData[idx].date);
        this.setValue("orderNumber",this.tourproData[idx].orderNumber);
        this.setValue("title",this.tourproData[idx].title);
        this.setValue("description",this.tourproData[idx].description);
        this.setValue("destination",this.tourproData[idx].destination);
    }

    updateTourProgram(){
        console.log("updateTourProgram",JSON.stringify( this.value));
        this.dataService.put(`api/tourprogram/${this.model.id}`, { ... this.value })
            .subscribe(res => {
                let id=this.tourproData[this.currentSelectedRowIdx].tourId;
                this.tourproData[this.currentSelectedRowIdx]=this.value;
                this.tourproData[this.currentSelectedRowIdx].tourId=id;
                this.toastr.success('Updated successfully.', 'Success');
            });
    }
    createTourProgram(){
        console.log("createTourProgram",JSON.stringify( this.value));
        this.dataService.post(`api/tourprogram`, { ...this.value})
            .subscribe(res => {
                this.dataService.get<any[]>(`api/tourprogram/ByTourId`,{tourId:this.value.tourId, orderNumber: this.value.orderNumber })
                    .subscribe(data => {
                        console.log("data",JSON.stringify(data));
                        this.tourproData=this.tourproData.filter(x=>x.orderNumber!=this.value.orderNumber);
                        this.tourproData=this.tourproData.concat(data);
                        this.toastr.success('Created successfully.', 'Success');
                    });
            });
    }
}

import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    Input,
    OnInit,
    TemplateRef,
    ViewChild
} from '@angular/core';
import {DatatableComponent} from '@swimlane/ngx-datatable';
import {clone} from 'lodash';

import {DataService, ModalService} from '@app/services';
import {FieldTypes, IAppTableOptions, IFieldConfig} from '@app/models';
import {ToastrService} from '@app/toastr';

@Component({
    selector: 'appc-table',
    templateUrl: './app-table.component.html',
    styleUrls: ['./app-table.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppTableComponent implements OnInit {
    @ViewChild('appTable', { static: true }) table: DatatableComponent;
    @ViewChild('formTemplate', { static: true }) formTemplate: TemplateRef<any>;
    @Input() options: IAppTableOptions<any>;
    // to preserve data after filter
    tempRows: any[];
    rowKeys: string[];
    constructor(
        private dataService: DataService,
        private toastr: ToastrService,
        private modalService: ModalService,
        private cd: ChangeDetectorRef) { }
    ngOnInit() {
        this.getData();
    }
    getData() {
        this.dataService.get<Array<any>>(this.options.apiUrl)
            .subscribe(data => {
                this.options.rows = data;
                this.rowKeys = Object.keys(data[0]);
                this.tempRows = [...data];
                this.toastr.info('Data loaded.', 'Info');
                this.cd.markForCheck();
            });
    }
    updateData(apiUrl){
        this.dataService.get<Array<any>>(apiUrl)
            .subscribe(data => {
                this.options.rows = data;
                this.rowKeys = Object.keys(data[0]);
                this.tempRows = [...data];
                this.toastr.info('Data loaded.', 'Info');
                this.cd.markForCheck();
            }); 
    }
    create() {
        this.createOrEdit()
            .then(() => {
                this.getData();
            }, () => { });
    }
    edit(row, rowIndex) {
        // Form Template
        this.createOrEdit(row, rowIndex)
            .then(() => {

            }, () => { });
    }
    acceptTourBooking(row, rowIndex) {
        // Form Template
        var agree=confirm("Are you sure to accept this tour booking?");
        if(agree){
            this.dataService.put<Tour>(`api/TourBooking/Censorship/${row.id}`).subscribe(x=>{
                console.log("Accept tour booking success!");
                this.updateData('api/TourBooking/Sensorships');
            },error => {  console.error(error);});
        }
    }
    acceptTour(row, rowIndex) {
        // Form Template
        var agree=confirm("Are you sure to accept this tour?");
        if(agree){
            this.dataService.put<Tour>(`api/tour/accepttour/${row.id}`).subscribe(x=>{
                console.log("Accept tour success!");
                this.updateData('api/tour/cencershiptour');
            },error => {  console.error(error);});
        }
    }
    acceptPost(row, rowIndex) {
        // Form Template
        var agree=confirm("Are you sure to accept this post?");
        if(agree){
            this.dataService.put<Tour>(`api/post/acceptpost/${row.id}`).subscribe(x=>{
                console.log("Accept Post success!");
                this.updateData('api/post/cencershippost');
            },error => {  console.error(error);});
        }
    }
    deleteTour(row, rowIndex) {
        // Form Template
        var agree=confirm("Are you sure close this tour?");
        if(agree){
            this.dataService.put<Tour>(`api/tour/statustour/${row.id}`).subscribe(x=>{
                console.log("Close tour success!");
                this.updateData('api/tour');
            },error => {  console.error(error);});
        }
    }
    delete(row, rowIndex) {
        this.modalService.confirm({
            title: 'Delete',
            message: 'Are you sure you want to delete this data?'
        }).then(() => {
            this.dataService.delete(`${this.options.apiUrl}/${row.id}`)
                .subscribe(() => {
                    this.options.rows = this.options.rows.filter(x => x.id !== row.id);
                    this.toastr.success('Deleted successfully.', 'Info');
                    this.cd.markForCheck();
                });
        }, () => { });
    }
    // Tìm kiếm theo ID
    updateFilter(event) {
        const val = event.toLowerCase();
        // filter our data
        const temp = this.tempRows.filter(d => {
            return d.id.toLowerCase().indexOf(val) !== -1 || !val;
        });
        // update the rows
        this.options.rows = temp;
        // Whenever the filter changes, always go back to the first page
        this.table.offset = 0;
    }
    // Tìm kiếm theo Tên
    FilterName(event) {
        const val = event.toLowerCase();
        const temp = this.tempRows.filter(d => {
            return d.name.toLowerCase().indexOf(val) !== -1 || !val;
        });
        this.options.rows = temp;
        this.table.offset = 0;
    }
    // Tìm kiếm ngày khởi hành
    FilterDepartrue(event) {
        const val = event.toLowerCase();
        const temp = this.tempRows.filter(d => {
            return d.departureDate.toLowerCase().indexOf(val) !== -1 || !val;
        });
        this.options.rows = temp;
        this.table.offset = 0;
    }
    toggleExpandRow(row) {
        console.log('Toggled Expand Row!', row);
        this.table.rowDetail.toggleExpandRow(row);
    }
    onDetailToggle(event) {
        console.log('Detail Toggled', event);
    }
    onPage(event) {
        console.log('Page event', event);
    }
    private createOrEdit(row = null, rowIndex = null): Promise<any> {
        const title = row ? 'Update' : 'Create';
        const fields = this.options.columns
            .filter(f => f.fieldType)
            .map(x => {
                var onSubmit;
                if(x.fieldType==FieldTypes.Button){
                    onSubmit=x.onSubmit.bind(this,this.modalService,this.formTemplate,x.subTableColumn,row);
                }else{
                    onSubmit=null;
                }
                const field: IFieldConfig = {
                    name: x.prop.toString(),
                    type: x.fieldType,
                    label: x.name,
                    validation: x.fieldValidations,
                    options: x.fieldOptions,
                    onSubmit:onSubmit
                };
                return field;
            });

        fields.push({
            name: 'button',
            type: FieldTypes.Button,
            label: title,
            onSubmit: row ? update.bind(this) : create.bind(this)
        });

        function update(form) {
            if (form.valid) {
                this.dataService.put(`${this.options.apiUrl}/${row.id}`, { ...form.value })
                    .subscribe(res => {
                        row = Object.assign({}, row, form.value);
                        this.options.rows[rowIndex] = row;
                        this.options.rows = this.options.rows.slice();
                        this.toastr.success('Updated successfully.', 'Success');
                        this.modalService.close();
                        this.cd.markForCheck();
                    });
            }
        }

        function create(form) {
            if (form.valid) {
                this.dataService.post(this.options.apiUrl, { ...form.value })
                    .subscribe(res => {
                        this.toastr.success('Created successfully.', 'Success');
                        this.modalService.close();
                    });
            }
        }
        const template = clone(<any>this.formTemplate);
        template.data = { formConfig: fields, formModel: (row || {}) };
        console.log("row:",JSON.stringify(row));
        return this.modalService.confirm({
            title,
            template
        });

    }
    private view(row = null, rowIndex = null): Promise<any> {
        const title = row ? 'Close' : "";
        const fields = this.options.columns
            .filter(f => f.fieldType)
            .map(x => {
                var onSubmit;
                if(x.fieldType==FieldTypes.Button){
                    onSubmit=x.onSubmit.bind(this,this.modalService,this.formTemplate,x.subTableColumn,row);
                }else{
                    onSubmit=null;
                }
                const field: IFieldConfig = {
                    name: x.prop.toString(),
                    type: x.fieldType,
                    label: x.name,
                    validation: x.fieldValidations,
                    options: x.fieldOptions,
                    onSubmit:onSubmit
                };
                return field;
            });

        fields.push({
            name: 'button',
            type: FieldTypes.Button,
            label: title,
            onSubmit: row ? close.bind(this) : 0
        });

        function close(form) {
            this.modalService.close();
        }
        const template = clone(<any>this.formTemplate);
        template.data = { formConfig: fields, formModel: (row || {}) };
        console.log("row:",JSON.stringify(row));
        return this.modalService.confirm({
            title,
            template
        });
    }
    viewContact(row, rowIndex) {
        this.view(row, rowIndex)
            .then(() => {

            }, () => { });
    }
    acceptBanner(row, rowIndex) {
        var agree=confirm("Are you sure to accept this banner?");
        if(agree){
            this.dataService.put<Tour>(`api/banner/acceptbanner/${row.id}`).subscribe(x=>{
                console.log("Accept Banner success!");
                this.updateData('api/banner/censorship');
            },error => {  console.error(error);});
        }
    }
}

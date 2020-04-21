import {Component, HostBinding, OnInit} from '@angular/core';
import {FieldTypes, IAppTableOptions} from "@app/models";
import {Validators} from "@angular/forms";
import {IProduct} from "@app/+examples/examples/crud-shop/crud-shop.models";

@Component({
  selector: 'appc-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  @HostBinding('class')
  elementClass = 'col-lg-10 col-md-9 bg-light content';
  options: IAppTableOptions<IProduct>;
  constructor() { }

  ngOnInit() {
    this.options = {
      title: 'Products',
      apiUrl: 'api/product',
      columns: [
        { prop: 'name', name: 'Name', fieldType: FieldTypes.Textbox, fieldValidations: [Validators.required] },
        { prop: 'description', name: 'Description', fieldType: FieldTypes.Textarea },
        { prop: 'icon', name: 'Icon', fieldType: FieldTypes.Textbox },
        { prop: 'buyingPrice', name: 'Buying price', fieldType: FieldTypes.Number, fieldValidations: [Validators.required] },
        { prop: 'sellingPrice', name: 'Selling price', fieldType: FieldTypes.Number, fieldValidations: [Validators.required] },
        { prop: 'unitsInStock', name: 'Units in stock', fieldType: FieldTypes.Number, fieldValidations: [Validators.required] },
        { prop: 'isActive', name: 'Is active', fieldType: FieldTypes.Checkbox },
        { prop: 'isDiscontinued', name: 'Is discontinued', fieldType: FieldTypes.Checkbox },
      ]
    };
  }

}

import {Component, Inject, Input, OnInit, Renderer2} from '@angular/core';
import {FieldTypes} from "@app/models";
import {Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {DataService} from "@app/services";
import {FormsService} from "@app/shared";
import {DOCUMENT} from "@angular/common";
import {Contact} from  "@app/models"
@Component({
  selector: 'appc-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {
  
  @Input() contact:Contact;
  constructor(@Inject("BASE_URL") private baseUrl: string,
              private router:Router,
              private _dataService:DataService,
              @Inject(DOCUMENT) private _document: Document,) { }

  ngOnInit() {
    this.contact={
      information:"",
      fullName:"",
      address:"",
      email:"",
      phone:"",
      title:"",
      content:"",
    };
  }
  postContact(){
    let ok=confirm("Are you sure send contact?");
    if(ok){
        console.log("Contact:", JSON.stringify(this.contact));
        this._dataService.post<Contact>(`${this.baseUrl}api/Contact`, JSON.stringify(this.contact)).subscribe(x => {
          alert("Contact tour success, you will come back to home ! Thank you");
          this.router.navigate(['']);
      });
    }
  }
}

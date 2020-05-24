import {Component, Inject, Input, OnInit, Renderer2} from '@angular/core';
import {FieldTypes} from "@app/models";
import {Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {DataService} from "@app/services";
import {FormsService} from "@app/shared";
import {DOCUMENT} from "@angular/common";
import {Contact} from  "@app/models"
declare var $: any;

@Component({
  selector: 'appc-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {
  resolved(captchaResponse: string) {
    console.log(`Resolved captcha with response: ${captchaResponse}`);
  }
  @Input() contact:Contact;
  constructor(@Inject("BASE_URL") public baseUrl: string,
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
  validate(){
    'use strict';
    window.addEventListener('load', function() {
      // Fetch all the forms we want to apply custom Bootstrap validation styles to
      var forms = document.getElementsByClassName('needs-validation');
      // Loop over them and prevent submission
      var validation = Array.prototype.filter.call(forms, function(form) {
        form.addEventListener('submit', function(event) {
          if (form.checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
          }
          form.classList.add('was-validated');
        }, false);
      });
    }, false);
  }
  postContact(){
  if (this.checkFormValid())
    {
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
  checkSpecialCharacterRegrex(value){
    if(value=="") return false;
    var regrex=new RegExp(/[`!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/);
    return regrex.test(value);
  }

  keyupValidate(value: string,maxLength=undefined,target=undefined) {
    var that=this;
    $('#'+target).keyup(function () {
      'use strict';
      if(that.checkSpecialCharacterRegrex(value)){
        this.setCustomValidity('Not accept special characters.');
      }else{
        if(!that.checkMaxLengthRegrex(value,maxLength)){
          this.setCustomValidity('Over length input');
        }else{
          this.setCustomValidity('');
        }
      }
    });
    $('#full_name').keyup(function () {
      'use strict';
      if(that.checkSpecialCharacterRegrex(value)){
        this.setCustomValidity('Not accept special characters.');
      }else{
        if(!that.checkMaxLengthRegrex(value,maxLength)){
          this.setCustomValidity('Over length input');
        }else{
          this.setCustomValidity('');
        }
      }
    });
    $('#mobilephone').keyup(function () {
      'use strict';
      if(!that.checkPhoneRegrex(value)){
        this.setCustomValidity('Invalid phone number format.');
      }else{
        this.setCustomValidity('');
      }
    });
    $('#email').keyup(function () {
      'use strict';
      if(!that.checkEmailRegrex(value)){
        this.setCustomValidity('Invalid phone number format.');
      }else{
        if(!that.checkMaxLengthRegrex(value,maxLength)){
          this.setCustomValidity('Over length input.');
        }else{
          this.setCustomValidity('');
        }
      }
    });
    $('#address').keyup(function () {
      'use strict';
      if(!that.checkMaxLengthRegrex(value,maxLength)){
        this.setCustomValidity('Over length input.');
      }else{
        this.setCustomValidity('');
      }
    });
    $('#title').keyup(function () {
      'use strict';
      if(!that.checkMaxLengthRegrex(value,maxLength)){
        this.setCustomValidity('Over length input.');
      }else{
        this.setCustomValidity('');
      }
    });
    $('#note').keyup(function () {
      'use strict';
      if(!that.checkMaxLengthRegrex(value,maxLength)){
        this.setCustomValidity('Over length input.');
      }else{
        this.setCustomValidity('');
      }
    });
  }

  checkPhoneRegrex(value){
    if(value=="") return true;
    var regrex=new RegExp(/^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/);
    return regrex.test(value);
  }
  checkEmailRegrex(value){
    if(value=="") return true;
    var regrex=new RegExp(/(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@[*[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+]*/);
    return regrex.test(value);
  }
  checkMaxLengthRegrex(value,maxLength){
    return value.length <= maxLength;
  }
  checkFormValid(){
    if(this._document.getElementById('full_name').className.indexOf("ng-invalid")!=-1){
      return false;
    }
    if(this._document.getElementById('mobilephone').className.indexOf("ng-invalid")!=-1){
      return false;
    }
    if(this._document.getElementById('email').className.indexOf("ng-invalid")!=-1){
      return false;
    }
    if(this._document.getElementById('address').className.indexOf("ng-invalid")!=-1){
      return false;
    }
    if(this._document.getElementById('note').className.indexOf("ng-invalid")!=-1){
      return false;
    }
    return true;
  }
}

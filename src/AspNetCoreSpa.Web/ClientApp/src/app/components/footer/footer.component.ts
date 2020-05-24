import {Component, Inject, OnInit, Renderer2} from '@angular/core';
import {AppService, AuthService} from "@app/services";
import {User} from "oidc-client";
import {DOCUMENT} from "@angular/common";
declare var $: any;
@Component({
    selector: 'appc-footer',
    styleUrls: ['./footer.component.scss'],
    templateUrl: './footer.component.html'
})
export class FooterComponent implements OnInit{
    constructor(
        @Inject("BASE_URL") public baseUrl: string,
        private authService: AuthService,
        private _renderer2: Renderer2,
        @Inject(DOCUMENT) private _document: Document
    ) { }
    public ngOnInit() {
        this.addGoogleTranslate();
    }
    get user(): User {
        return this.authService.user;

    }
    get IsNotUser():boolean{
        var isNotUser=false;
        try{
            isNotUser = this.authService.user.profile.role.some(x=>{
                return x=="admin"||x=="Admin" ||x=="staff"||x=="Staff"
            });
        }catch (e) {
            isNotUser=false;
        }
        return isNotUser;
    }
    private addGoogleTranslate() {
        let script = this._renderer2.createElement('script');
        script.type = `text/javascript`;
        // console.log(id);
        script.text = `
            {
                  function googleTranslateElementInit() {
      new google.translate.TranslateElement({pageLanguage: 'en', includedLanguages: 'ar,en,es,jv,ko,pa,pt,ru,zh-CN,th,vi', layout: google.translate.TranslateElement.InlineLayout.SIMPLE, autoDisplay: false}, 'google_translate_element');
        $(".goog-te-gadget-simple").css('background-color','#333333');
        $(".goog-te-gadget-simple").css('border-style','none');
    }
    $('document').ready(function () {
// RESTYLE THE DROPDOWN MENU
    $('#google_translate_element').on("click", function () {
        // Change font family and color
        $("iframe").contents().find(".goog-te-menu2-item div, .goog-te-menu2-item:link div, .goog-te-menu2-item:visited div, .goog-te-menu2-item:active div, .goog-te-menu2 *")
            .css({
                'color': '#544F4B',
                'font-family': 'Roboto',
                'width':'100%'
            });
        // Change menu's padding
        $("iframe").contents().find('.goog-te-menu2-item-selected').css ('display', 'none');

        // Change menu's padding
        $("iframe").contents().find('.goog-te-menu2').css ('padding', '0px');
      
        // Change the padding of the languages
        $("iframe").contents().find('.goog-te-menu2-item div').css('padding', '20px');
      
        // Change the width of the languages
        $("iframe").contents().find('.goog-te-menu2-item').css('width', '100%');
        $("iframe").contents().find('td').css('width', '100%');
      
        // Change hover effects
        $("iframe").contents().find(".goog-te-menu2-item div").hover(function () {
            $(this).css('background-color', '#4385F5').find('span.text').css('color', 'white');
        }, function () {
            $(this).css('background-color', 'white').find('span.text').css('color', '#544F4B');
        });

        // Change Google's default blue border
        $("iframe").contents().find('.goog-te-menu2').css('border', 'none');

        // Change the iframe's box shadow
        $(".goog-te-menu-frame").css('box-shadow', '0 16px 24px 2px rgba(0, 0, 0, 0.14), 0 6px 30px 5px rgba(0, 0, 0, 0.12), 0 8px 10px -5px rgba(0, 0, 0, 0.3)');
        
        $(".goog-te-gadget-simple").css('background-color','#333333');
        $(".goog-te-gadget-simple").css('border-style','none');
        
        // Change the iframe's size and position?
        $(".goog-te-menu-frame").css({
            'height': '100%',
            'width': '100%',
            'top': '0px'
        });
        // Change iframes's size
        $("iframe").contents().find('.goog-te-menu2').css({
            'height': '100%',
            'width': '100%'
        });
    });
    });
            }
        `;
        let script2 = this._renderer2.createElement('script');
        script2.type = `text/javascript`;
        script2.src =`//translate.google.com/translate_a/element.js?cb=googleTranslateElementInit`;
        var parentEle=this._document.getElementById("main");
        parentEle&&this._renderer2.appendChild(parentEle, script);
        var parentEle=this._document.getElementById("main");
        parentEle&&this._renderer2.appendChild(parentEle, script2);
    }
}

import {Component, Inject, OnInit, Renderer2} from '@angular/core';
import {DOCUMENT} from "@angular/common";
import {DataService} from "@app/services";
declare var $: any;
@Component({
    selector: 'appc-home-component',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit{
    
    constructor(
        @Inject("BASE_URL") public baseUrl: string,
        private _renderer2: Renderer2,
        private _dataService: DataService,
        @Inject(DOCUMENT) private _document: Document,
    ) { }

    public ngOnInit() {

        $('.carousel').carousel({
            interval: 5000
        })
    }
    
}
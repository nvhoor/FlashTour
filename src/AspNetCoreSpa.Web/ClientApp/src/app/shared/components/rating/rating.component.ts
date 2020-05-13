import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'appc-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.scss']
})
export class RatingComponent implements OnInit {
  currentRate = 8;
  constructor() { }

  ngOnInit() {
  }

  rateTourChange() {
    console.log("rateTourChange",this.currentRate);
  }
}


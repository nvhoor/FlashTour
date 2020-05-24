import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingSuccessComponent } from './booking-success.component';

describe('BookingSuccessComponent', () => {
  let component: BookingSuccessComponent;
  let fixture: ComponentFixture<BookingSuccessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookingSuccessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookingSuccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

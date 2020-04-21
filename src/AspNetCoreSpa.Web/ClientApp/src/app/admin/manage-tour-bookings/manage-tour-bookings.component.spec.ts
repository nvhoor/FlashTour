import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageTourBookingsComponent } from './manage-tour-bookings.component';

describe('ManageTourBookingsComponent', () => {
  let component: ManageTourBookingsComponent;
  let fixture: ComponentFixture<ManageTourBookingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageTourBookingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageTourBookingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageTourBookingsStaffComponent } from './manage-tour-bookings-staff.component';

describe('ManageTourBookingsStaffComponent', () => {
  let component: ManageTourBookingsStaffComponent;
  let fixture: ComponentFixture<ManageTourBookingsStaffComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageTourBookingsStaffComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageTourBookingsStaffComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageToursStaffComponent } from './manage-tours-staff.component';

describe('ManageToursStaffComponent', () => {
  let component: ManageToursStaffComponent;
  let fixture: ComponentFixture<ManageToursStaffComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageToursStaffComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageToursStaffComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

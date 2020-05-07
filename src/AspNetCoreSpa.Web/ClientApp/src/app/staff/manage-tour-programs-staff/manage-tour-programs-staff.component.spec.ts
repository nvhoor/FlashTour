import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageTourProgramsStaffComponent } from './manage-tour-programs-staff.component';

describe('ManageTourProgramsStaffComponent', () => {
  let component: ManageTourProgramsStaffComponent;
  let fixture: ComponentFixture<ManageTourProgramsStaffComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageTourProgramsStaffComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageTourProgramsStaffComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

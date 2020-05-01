import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AutoLoginStaffComponent } from './auto-login-staff.component';

describe('AutoLoginStaffComponent', () => {
  let component: AutoLoginStaffComponent;
  let fixture: ComponentFixture<AutoLoginStaffComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AutoLoginStaffComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AutoLoginStaffComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AutoLoginAdminComponent } from './auto-login-admin.component';

describe('AutoLoginAdminComponent', () => {
  let component: AutoLoginAdminComponent;
  let fixture: ComponentFixture<AutoLoginAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AutoLoginAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AutoLoginAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

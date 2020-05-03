import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagePostsStaffComponent } from './manage-posts-staff.component';

describe('ManagePostsStaffComponent', () => {
  let component: ManagePostsStaffComponent;
  let fixture: ComponentFixture<ManagePostsStaffComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManagePostsStaffComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManagePostsStaffComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

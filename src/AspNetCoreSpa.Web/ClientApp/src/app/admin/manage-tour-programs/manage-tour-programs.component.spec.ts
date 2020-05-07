import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageTourProgramsComponent } from './manage-tour-programs.component';

describe('ManageTourProgramsComponent', () => {
  let component: ManageTourProgramsComponent;
  let fixture: ComponentFixture<ManageTourProgramsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageTourProgramsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageTourProgramsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

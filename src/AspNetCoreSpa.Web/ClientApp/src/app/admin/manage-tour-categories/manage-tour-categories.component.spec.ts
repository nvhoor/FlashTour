import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageTourCategoriesComponent } from './manage-tour-categories.component';

describe('ManageTourCategoriesComponent', () => {
  let component: ManageTourCategoriesComponent;
  let fixture: ComponentFixture<ManageTourCategoriesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageTourCategoriesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageTourCategoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

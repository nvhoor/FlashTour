import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TourCategoryComponent } from './tour-category.component';

describe('TourCategoryComponent', () => {
  let component: TourCategoryComponent;
  let fixture: ComponentFixture<TourCategoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TourCategoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TourCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

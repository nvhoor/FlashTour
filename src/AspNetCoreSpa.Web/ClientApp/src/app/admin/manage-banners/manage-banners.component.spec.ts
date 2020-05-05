import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageBannersComponent } from './manage-banners.component';

describe('ManageBannersComponent', () => {
  let component: ManageBannersComponent;
  let fixture: ComponentFixture<ManageBannersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageBannersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageBannersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
